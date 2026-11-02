using System;
using System.Windows.Forms;

namespace OpenGL
{
    class cOGL
    {
        // ── window handles ───────────────────────────────────────────────
        Control p;
        int Width, Height;
        uint m_uint_HWND = 0;
        uint m_uint_DC = 0;
        uint m_uint_RC = 0;
        public uint HWND { get { return m_uint_HWND; } }
        public uint DC { get { return m_uint_DC; } }
        public uint RC { get { return m_uint_RC; } }

        // ── camera ───────────────────────────────────────────────────────
        public float[] ScrollValue = new float[10];
        double[] AccumulatedRotationsTranslations = new double[16];
        public int intOptionC = 0;

        // ── projection ───────────────────────────────────────────────────
        public bool isPerspective = false;
        public double FOV = 45.0;

        // ── lighting ─────────────────────────────────────────────────────
        public float LightR = 0.9f, LightG = 0.9f, LightB = 0.9f;
        public float Ambient = 0.3f;
        public float LightX = 2.0f, LightZ = 2.0f;
        public bool LightEnabled = true;
        public bool SpotlightEnabled = false;

        // ── display ──────────────────────────────────────────────────────
        public bool ShowAxes = true;

        // ── scene objects ─────────────────────────────────────────────────
        ChessBoard board = new ChessBoard();
        ChessPiece[] pieces;

        // ── selection & animation state ───────────────────────────────────
        ChessPiece selectedPiece = null;
        public bool IsAnimating = false;

        // ── constructor ──────────────────────────────────────────────────
        public cOGL(Control pb)
        {
            p = pb;
            Width = p.Width;
            Height = p.Height;
            InitializeGL();
        }

        ~cOGL()
        {
            ChessPiece.Cleanup();
            WGL.wglDeleteContext(m_uint_RC);
        }

        // ── context setup ────────────────────────────────────────────────
        void InitializeGL()
        {
            m_uint_HWND = (uint)p.Handle.ToInt32();
            m_uint_DC = WGL.GetDC(m_uint_HWND);
            WGL.wglSwapBuffers(m_uint_DC);

            WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor(ref pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER;
            pfd.iPixelType = (byte)WGL.PFD_TYPE_RGBA;
            pfd.cColorBits = 32;
            pfd.cDepthBits = 32;
            pfd.iLayerType = (byte)WGL.PFD_MAIN_PLANE;

            int pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
            if (pixelFormatIndex == 0) { MessageBox.Show("Unable to retrieve pixel format"); return; }
            if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pfd) == 0) { MessageBox.Show("Unable to set pixel format"); return; }

            m_uint_RC = WGL.wglCreateContext(m_uint_DC);
            if (m_uint_RC == 0) { MessageBox.Show("Unable to get rendering context"); return; }
            if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0) { MessageBox.Show("Unable to make rendering context current"); return; }

            InitRenderingGL();
        }

        void InitRenderingGL()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0) return;
            if (Width == 0 || Height == 0) return;

            GL.glClearColor(0.1f, 0.1f, 0.2f, 0.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);
            GL.glEnable(GL.GL_COLOR_MATERIAL);
            GL.glEnable(GL.GL_NORMALIZE);
            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_LIGHTING);

            GL.glViewport(0, 0, Width, Height);
            SetProjection();

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTranslations);

            board.Init();
            ChessPiece.InitLists();
            SetupPieces();
        }

        // ── click handler — called from Form1 panel MouseClick ────────────
        // converts panel pixel (px, py) → board col/row → select or move
        public void OnBoardClick(int px, int py)
        {
            if (IsAnimating) return;

            // ── ray picking: unproject two points to get a ray ────────────────
            double[] modelView = new double[16];
            double[] projection = new double[16];
            int[] viewport = new int[4];

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, modelView);
            GL.glGetDoublev(GL.GL_PROJECTION_MATRIX, projection);
            GL.glGetIntegerv(GL.GL_VIEWPORT, viewport);

            // flip Y — OpenGL origin is bottom-left, Windows is top-left
            int flippedY = viewport[3] - py;

            // unproject at near plane (z=0) and far plane (z=1)
            double nearX, nearY, nearZ;
            double farX, farY, farZ;

            GLU.gluUnProject(px, flippedY, 0.0,
                             modelView, projection, viewport,
                             out nearX, out nearY, out nearZ);

            GLU.gluUnProject(px, flippedY, 1.0,
                             modelView, projection, viewport,
                             out farX, out farY, out farZ);

            // ── intersect ray with Y=0 plane (the board surface) ─────────────
            double dirX = farX - nearX;
            double dirY = farY - nearY;
            double dirZ = farZ - nearZ;

            if (Math.Abs(dirY) < 0.0001) return;  // ray parallel to board

            double t = -nearY / dirY;             // parameter where Y=0
            double hitX = nearX + t * dirX;
            double hitZ = nearZ + t * dirZ;

            // ── convert world X/Z to board col/row ────────────────────────────
            // board runs from -4 to +4 on both axes, 8 squares total
            int col = (int)Math.Floor((hitX + 4.0) / 1.0);
            int row = (int)Math.Floor((hitZ + 4.0) / 1.0);

            // clamp and validate
            if (col < 0 || col > 7 || row < 0 || row > 7) return;

            // ── rest of selection logic (unchanged) ───────────────────────────
            if (selectedPiece == null)
            {
                ChessPiece clicked = GetPieceAt(col, row);
                if (clicked != null)
                {
                    selectedPiece = clicked;
                    selectedPiece.IsSelected = true;
                }
                Draw();
                return;
            }

            ChessPiece target = GetPieceAt(col, row);

            if (target != null && target == selectedPiece)
            {
                selectedPiece.IsSelected = false;
                selectedPiece = null;
            }
            else if (target != null && target.Color == selectedPiece.Color)
            {
                selectedPiece.IsSelected = false;
                selectedPiece = target;
                selectedPiece.IsSelected = true;
            }
            else
            {
                if (target != null) RemovePiece(target);
                selectedPiece.MoveTo(col, row);
                selectedPiece.IsSelected = false;
                selectedPiece = null;
                IsAnimating = true;
            }

            Draw();
        }

        // ── find piece at board square (null if empty) ────────────────────
        ChessPiece GetPieceAt(int col, int row)
        {
            if (pieces == null) return null;
            foreach (ChessPiece p in pieces)
                if (p != null && p.Col == col && p.Row == row)
                    return p;
            return null;
        }

        // ── remove a captured piece from the array ────────────────────────
        void RemovePiece(ChessPiece toRemove)
        {
            if (pieces == null) return;
            for (int i = 0; i < pieces.Length; i++)
                if (pieces[i] == toRemove)
                    pieces[i] = null;
        }

        // ── animation tick — called by Form1 timer ────────────────────────
        public void AnimationTick()
        {
            if (!IsAnimating || pieces == null) return;

            bool anyStillMoving = false;
            foreach (ChessPiece piece in pieces)
                if (piece != null && piece.AnimationTick())
                    anyStillMoving = true;

            if (!anyStillMoving)
                IsAnimating = false;

            Draw();
        }

        // ── piece setup ───────────────────────────────────────────────────
        void SetupPieces()
        {
            pieces = new ChessPiece[32];
            int i = 0;

            pieces[i++] = new ChessPiece(PieceType.Rook, PieceColor.Black, 0, 7);
            pieces[i++] = new ChessPiece(PieceType.Knight, PieceColor.Black, 1, 7);
            pieces[i++] = new ChessPiece(PieceType.Bishop, PieceColor.Black, 2, 7);
            pieces[i++] = new ChessPiece(PieceType.King, PieceColor.Black, 3, 7);
            pieces[i++] = new ChessPiece(PieceType.Queen, PieceColor.Black, 4, 7);
            pieces[i++] = new ChessPiece(PieceType.Bishop, PieceColor.Black, 5, 7);
            pieces[i++] = new ChessPiece(PieceType.Knight, PieceColor.Black, 6, 7);
            pieces[i++] = new ChessPiece(PieceType.Rook, PieceColor.Black, 7, 7);
            for (int c = 0; c < 8; c++)
                pieces[i++] = new ChessPiece(PieceType.Pawn, PieceColor.Black, c, 6);

            pieces[i++] = new ChessPiece(PieceType.Rook, PieceColor.White, 0, 0);
            pieces[i++] = new ChessPiece(PieceType.Knight, PieceColor.White, 1, 0);
            pieces[i++] = new ChessPiece(PieceType.Bishop, PieceColor.White, 2, 0);
            pieces[i++] = new ChessPiece(PieceType.King, PieceColor.White, 3, 0);
            pieces[i++] = new ChessPiece(PieceType.Queen, PieceColor.White, 4, 0);
            pieces[i++] = new ChessPiece(PieceType.Bishop, PieceColor.White, 5, 0);
            pieces[i++] = new ChessPiece(PieceType.Knight, PieceColor.White, 6, 0);
            pieces[i++] = new ChessPiece(PieceType.Rook, PieceColor.White, 7, 0);
            for (int c = 0; c < 8; c++)
                pieces[i++] = new ChessPiece(PieceType.Pawn, PieceColor.White, c, 1);
        }

        // ── projection ───────────────────────────────────────────────────
        public void SetProjection()
        {
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();
            double aspect = (Width > 0 && Height > 0) ? (double)Width / Height : 1.0;
            if (isPerspective)
                GLU.gluPerspective(FOV, aspect, 0.4, 100.0);
            else
                GL.glOrtho(-10.0 * aspect, 10.0 * aspect, -10.0, 10.0, 0.1, 100.0);
            GL.glMatrixMode(GL.GL_MODELVIEW);
        }

        public void ToggleProjection()
        {
            isPerspective = !isPerspective;
            SetProjection();
            Draw();
        }

        public void OnResize()
        {
            Width = p.Width;
            Height = p.Height;
            GL.glViewport(0, 0, Width, Height);
            SetProjection();
            Draw();
        }

        // ── lighting ─────────────────────────────────────────────────────
        void ApplyLighting()
        {
            if (!LightEnabled) { GL.glDisable(GL.GL_LIGHT0); return; }
            GL.glEnable(GL.GL_LIGHT0);

            float[] pos = { LightX, 10.0f, LightZ, 1.0f };
            float[] diffuse = { LightR, LightG, LightB, 1.0f };
            float[] specular = { LightR, LightG, LightB, 1.0f };
            float[] ambient = { Ambient, Ambient, Ambient, 1.0f };

            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, pos);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, diffuse);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, specular);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, ambient);

            if (SpotlightEnabled)
            {
                float[] spotDir = { -LightX, -10.0f, -LightZ };
                GL.glLightfv(GL.GL_LIGHT0, GL.GL_SPOT_DIRECTION, spotDir);
                GL.glLightf(GL.GL_LIGHT0, GL.GL_SPOT_CUTOFF, 35.0f);
                GL.glLightf(GL.GL_LIGHT0, GL.GL_SPOT_EXPONENT, 8.0f);
            }
            else
            {
                GL.glLightf(GL.GL_LIGHT0, GL.GL_SPOT_CUTOFF, 180.0f);
            }
        }

        // ── main draw ────────────────────────────────────────────────────
        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0) return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glLoadIdentity();

            GLU.gluLookAt(
                ScrollValue[0], ScrollValue[1], ScrollValue[2],
                ScrollValue[3], ScrollValue[4], ScrollValue[5],
                ScrollValue[6], ScrollValue[7], ScrollValue[8]);

            double[] beforeTransform = new double[16];
            double[] currentTransform = new double[16];

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, beforeTransform);
            GL.glLoadIdentity();

            if (intOptionC != 0)
            {
                float delta = 5.0f * Math.Abs(intOptionC) / intOptionC;
                switch (Math.Abs(intOptionC))
                {
                    case 1: GL.glRotatef(delta, 1, 0, 0); break;
                    case 2: GL.glRotatef(delta, 0, 1, 0); break;
                    case 3: GL.glRotatef(delta, 0, 0, 1); break;
                    case 4: GL.glTranslatef(delta / 20, 0, 0); break;
                    case 5: GL.glTranslatef(0, delta / 20, 0); break;
                    case 6: GL.glTranslatef(0, 0, delta / 20); break;
                }
            }

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, currentTransform);
            GL.glLoadMatrixd(AccumulatedRotationsTranslations);
            GL.glMultMatrixd(currentTransform);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTranslations);
            GL.glLoadMatrixd(beforeTransform);
            GL.glMultMatrixd(AccumulatedRotationsTranslations);

            ApplyLighting();

            if (ShowAxes) DrawAxes();
            board.Draw();

            GL.glEnable(GL.GL_LIGHTING);
            if (pieces != null)
                foreach (ChessPiece piece in pieces)
                    if (piece != null) piece.Draw();

            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);
        }

        void DrawAxes()
        {
            GL.glDisable(GL.GL_LIGHTING);
            GL.glBegin(GL.GL_LINES);
            GL.glColor3f(1, 0, 0); GL.glVertex3f(0, 0, 0); GL.glVertex3f(3, 0, 0);
            GL.glColor3f(0, 1, 0); GL.glVertex3f(0, 0, 0); GL.glVertex3f(0, 3, 0);
            GL.glColor3f(0, 0, 1); GL.glVertex3f(0, 0, 0); GL.glVertex3f(0, 0, 3);
            GL.glEnd();
            GL.glEnable(GL.GL_LIGHTING);
        }
    }
}