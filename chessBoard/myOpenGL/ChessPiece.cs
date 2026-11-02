using System;

namespace OpenGL
{
    enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King }
    enum PieceColor { White, Black }

    class ChessPiece
    {
        // ── identity ─────────────────────────────────────────────────────
        public PieceType Type;
        public PieceColor Color;

        // board position (col 0-7, row 0-7)
        public int Col;
        public int Row;

        // ── animation state ───────────────────────────────────────────────
        // world-space position (smoothly interpolated during animation)
        public float WorldX;
        public float WorldZ;

        // target world position
        float targetX;
        float targetZ;

        public bool IsAnimating = false;
        const float ANIM_SPEED = 0.08f;   // units per tick

        // ── selection state ───────────────────────────────────────────────
        public bool IsSelected = false;

        // ── display lists ─────────────────────────────────────────────────
        static uint LIST_PAWN = 0;
        static uint LIST_ROOK = 0;
        static uint LIST_KNIGHT = 0;
        static uint LIST_BISHOP = 0;
        static uint LIST_QUEEN = 0;
        static uint LIST_KING = 0;
        static bool listsReady = false;
        static GLUquadric quad;

        // ── constructor ──────────────────────────────────────────────────
        public ChessPiece(PieceType type, PieceColor color, int col, int row)
        {
            Type = type;
            Color = color;
            Col = col;
            Row = row;

            // initialise world position to board square center
            WorldX = ChessBoard.ColToX(col);
            WorldZ = ChessBoard.RowToZ(row);
        }

        // ── start moving to a new square ─────────────────────────────────
        public void MoveTo(int newCol, int newRow)
        {
            Col = newCol;
            Row = newRow;
            targetX = ChessBoard.ColToX(newCol);
            targetZ = ChessBoard.RowToZ(newRow);
            IsAnimating = true;
        }

        // ── call every timer tick to advance animation ────────────────────
        // returns true when animation is still running
        public bool AnimationTick()
        {
            if (!IsAnimating) return false;

            float dx = targetX - WorldX;
            float dz = targetZ - WorldZ;
            float dist = (float)Math.Sqrt(dx * dx + dz * dz);

            if (dist < ANIM_SPEED)
            {
                // snap to target
                WorldX = targetX;
                WorldZ = targetZ;
                IsAnimating = false;
                return false;
            }

            // move toward target
            WorldX += (dx / dist) * ANIM_SPEED;
            WorldZ += (dz / dist) * ANIM_SPEED;
            return true;
        }

        // ── init / cleanup ────────────────────────────────────────────────
        public static void InitLists()
        {
            if (listsReady) return;
            quad = GLU.gluNewQuadric();
            GLU.gluQuadricNormals(quad, GLU.GLU_SMOOTH);

            LIST_PAWN = GL.glGenLists(6);
            LIST_ROOK = LIST_PAWN + 1;
            LIST_KNIGHT = LIST_PAWN + 2;
            LIST_BISHOP = LIST_PAWN + 3;
            LIST_QUEEN = LIST_PAWN + 4;
            LIST_KING = LIST_PAWN + 5;

            BuildPawn();
            BuildRook();
            BuildKnight();
            BuildBishop();
            BuildQueen();
            BuildKing();

            listsReady = true;
        }

        public static void Cleanup()
        {
            if ((object)quad != null)
                GLU.gluDeleteQuadric(quad);
        }

        // ── draw ──────────────────────────────────────────────────────────
        public void Draw()
        {
            GL.glPushMatrix();

            // use smooth world position for animation
            GL.glTranslatef(WorldX, 0.0f, WorldZ);
            GL.glRotatef(-90.0f, 1.0f, 0.0f, 0.0f);

            GL.glDisable(GL.GL_COLOR_MATERIAL);

            if (IsSelected)
                SetSelectedMaterial();
            else
                SetMaterial();

            switch (Type)
            {
                case PieceType.Pawn: GL.glCallList(LIST_PAWN); break;
                case PieceType.Rook: GL.glCallList(LIST_ROOK); break;
                case PieceType.Knight: GL.glCallList(LIST_KNIGHT); break;
                case PieceType.Bishop: GL.glCallList(LIST_BISHOP); break;
                case PieceType.Queen: GL.glCallList(LIST_QUEEN); break;
                case PieceType.King: GL.glCallList(LIST_KING); break;
            }

            GL.glEnable(GL.GL_COLOR_MATERIAL);

            GL.glPopMatrix();
        }

        // ── materials ─────────────────────────────────────────────────────
        void SetMaterial()
        {
            if (Color == PieceColor.White)
            {
                float[] ambient = { 0.8f, 0.8f, 0.75f, 1.0f };
                float[] diffuse = { 0.95f, 0.95f, 0.9f, 1.0f };
                float[] specular = { 1.0f, 1.0f, 1.0f, 1.0f };
                float[] shine = { 80.0f };
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT, ambient);
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_DIFFUSE, diffuse);
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, specular);
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_SHININESS, shine);
            }
            else
            {
                float[] ambient = { 0.05f, 0.05f, 0.05f, 1.0f };
                float[] diffuse = { 0.1f, 0.1f, 0.1f, 1.0f };
                float[] specular = { 0.6f, 0.6f, 0.6f, 1.0f };
                float[] shine = { 60.0f };
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT, ambient);
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_DIFFUSE, diffuse);
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, specular);
                GL.glMaterialfv(GL.GL_FRONT, GL.GL_SHININESS, shine);
            }
        }

        // yellow-gold highlight for selected piece
        void SetSelectedMaterial()
        {
            float[] ambient = { 0.4f, 0.35f, 0.0f, 1.0f };
            float[] diffuse = { 0.9f, 0.8f, 0.1f, 1.0f };
            float[] specular = { 1.0f, 1.0f, 0.5f, 1.0f };
            float[] shine = { 90.0f };
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT, ambient);
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_DIFFUSE, diffuse);
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, specular);
            GL.glMaterialfv(GL.GL_FRONT, GL.GL_SHININESS, shine);
        }

        // ── piece geometry (unchanged from Phase 3) ───────────────────────
        static void BuildPawn()
        {
            GL.glNewList(LIST_PAWN, GL.GL_COMPILE);
            GL.glPushMatrix();
            GLU.gluDisk(quad, 0, 0.35f, 20, 1);
            GLU.gluCylinder(quad, 0.35f, 0.28f, 0.12f, 20, 4);
            GL.glTranslatef(0, 0, 0.12f);
            GLU.gluDisk(quad, 0, 0.28f, 20, 1);
            GLU.gluCylinder(quad, 0.28f, 0.18f, 0.45f, 20, 6);
            GL.glTranslatef(0, 0, 0.45f);
            GLU.gluCylinder(quad, 0.18f, 0.16f, 0.12f, 20, 2);
            GL.glTranslatef(0, 0, 0.12f);
            GLU.gluSphere(quad, 0.22f, 20, 20);
            GL.glPopMatrix();
            GL.glEndList();
        }

        static void BuildRook()
        {
            GL.glNewList(LIST_ROOK, GL.GL_COMPILE);
            GL.glPushMatrix();
            GLU.gluDisk(quad, 0, 0.38f, 20, 1);
            GLU.gluCylinder(quad, 0.38f, 0.30f, 0.10f, 20, 2);
            GL.glTranslatef(0, 0, 0.10f);
            GLU.gluDisk(quad, 0, 0.30f, 20, 1);
            GLU.gluCylinder(quad, 0.30f, 0.28f, 0.60f, 20, 6);
            GL.glTranslatef(0, 0, 0.60f);
            GLU.gluDisk(quad, 0, 0.32f, 20, 1);
            GLU.gluCylinder(quad, 0.32f, 0.32f, 0.10f, 20, 2);
            GL.glTranslatef(0, 0, 0.10f);
            GLU.gluDisk(quad, 0, 0.32f, 20, 1);
            float bR = 0.27f, bW = 0.10f, bH = 0.14f;
            for (int i = 0; i < 4; i++)
            {
                float angle = i * 90.0f * (float)Math.PI / 180.0f;
                float bx = bR * (float)Math.Cos(angle);
                float by = bR * (float)Math.Sin(angle);
                GL.glPushMatrix();
                GL.glTranslatef(bx - bW / 2, by - bW / 2, 0);
                DrawBox(bW, bW, bH);
                GL.glPopMatrix();
            }
            GL.glPopMatrix();
            GL.glEndList();
        }

        static void BuildKnight()
        {
            GL.glNewList(LIST_KNIGHT, GL.GL_COMPILE);
            GL.glPushMatrix();
            GLU.gluDisk(quad, 0, 0.36f, 20, 1);
            GLU.gluCylinder(quad, 0.36f, 0.28f, 0.10f, 20, 2);
            GL.glTranslatef(0, 0, 0.10f);
            GLU.gluDisk(quad, 0, 0.28f, 20, 1);
            GLU.gluCylinder(quad, 0.28f, 0.22f, 0.40f, 20, 4);
            GL.glTranslatef(0, 0, 0.40f);
            GL.glPushMatrix();
            GL.glScalef(1.0f, 1.0f, 1.3f);
            GLU.gluSphere(quad, 0.22f, 20, 20);
            GL.glPopMatrix();
            GL.glTranslatef(0, 0, 0.22f);
            GL.glPushMatrix();
            GL.glTranslatef(0.10f, 0, 0.18f);
            GL.glScalef(1.3f, 0.75f, 1.1f);
            GLU.gluSphere(quad, 0.18f, 20, 20);
            GL.glPopMatrix();
            GL.glPushMatrix();
            GL.glTranslatef(0.22f, 0, 0.18f);
            GLU.gluSphere(quad, 0.09f, 16, 16);
            GL.glPopMatrix();
            GL.glPopMatrix();
            GL.glEndList();
        }

        static void BuildBishop()
        {
            GL.glNewList(LIST_BISHOP, GL.GL_COMPILE);
            GL.glPushMatrix();
            GLU.gluDisk(quad, 0, 0.36f, 20, 1);
            GLU.gluCylinder(quad, 0.36f, 0.27f, 0.10f, 20, 2);
            GL.glTranslatef(0, 0, 0.10f);
            GLU.gluDisk(quad, 0, 0.27f, 20, 1);
            GLU.gluCylinder(quad, 0.27f, 0.17f, 0.45f, 20, 6);
            GL.glTranslatef(0, 0, 0.45f);
            GLU.gluDisk(quad, 0, 0.17f, 20, 1);
            GLU.gluCylinder(quad, 0.17f, 0.12f, 0.25f, 20, 4);
            GL.glTranslatef(0, 0, 0.25f);
            GLU.gluSphere(quad, 0.14f, 20, 20);
            GL.glTranslatef(0, 0, 0.12f);
            GLU.gluCylinder(quad, 0.06f, 0.0f, 0.18f, 16, 4);
            GL.glPopMatrix();
            GL.glEndList();
        }

        static void BuildQueen()
        {
            GL.glNewList(LIST_QUEEN, GL.GL_COMPILE);
            GL.glPushMatrix();
            GLU.gluDisk(quad, 0, 0.40f, 20, 1);
            GLU.gluCylinder(quad, 0.40f, 0.30f, 0.10f, 20, 2);
            GL.glTranslatef(0, 0, 0.10f);
            GLU.gluDisk(quad, 0, 0.30f, 20, 1);
            GLU.gluCylinder(quad, 0.30f, 0.18f, 0.50f, 20, 8);
            GL.glTranslatef(0, 0, 0.50f);
            GLU.gluSphere(quad, 0.20f, 20, 20);
            GL.glTranslatef(0, 0, 0.16f);
            GLU.gluCylinder(quad, 0.18f, 0.22f, 0.25f, 20, 4);
            GL.glTranslatef(0, 0, 0.25f);
            GLU.gluDisk(quad, 0, 0.22f, 20, 1);
            GLU.gluCylinder(quad, 0.22f, 0.20f, 0.08f, 20, 2);
            GL.glTranslatef(0, 0, 0.08f);
            GLU.gluDisk(quad, 0, 0.20f, 20, 1);
            int numSpikes = 5;
            float spikeR = 0.17f, spikeH = 0.20f;
            for (int i = 0; i < numSpikes; i++)
            {
                float angle = i * (360.0f / numSpikes) * (float)Math.PI / 180.0f;
                GL.glPushMatrix();
                GL.glTranslatef(spikeR * (float)Math.Cos(angle), spikeR * (float)Math.Sin(angle), 0);
                GLU.gluCylinder(quad, 0.04f, 0.0f, spikeH, 12, 4);
                GL.glPopMatrix();
            }
            GL.glPushMatrix();
            GL.glTranslatef(0, 0, 0.08f);
            GLU.gluSphere(quad, 0.07f, 16, 16);
            GL.glPopMatrix();
            GL.glPopMatrix();
            GL.glEndList();
        }

        static void BuildKing()
        {
            GL.glNewList(LIST_KING, GL.GL_COMPILE);
            GL.glPushMatrix();
            GLU.gluDisk(quad, 0, 0.40f, 20, 1);
            GLU.gluCylinder(quad, 0.40f, 0.30f, 0.10f, 20, 2);
            GL.glTranslatef(0, 0, 0.10f);
            GLU.gluDisk(quad, 0, 0.30f, 20, 1);
            GLU.gluCylinder(quad, 0.30f, 0.18f, 0.55f, 20, 8);
            GL.glTranslatef(0, 0, 0.55f);
            GLU.gluSphere(quad, 0.20f, 20, 20);
            GL.glTranslatef(0, 0, 0.16f);
            GLU.gluCylinder(quad, 0.18f, 0.20f, 0.32f, 20, 4);
            GL.glTranslatef(0, 0, 0.32f);
            GLU.gluDisk(quad, 0, 0.20f, 20, 1);
            GLU.gluCylinder(quad, 0.20f, 0.18f, 0.06f, 20, 2);
            GL.glTranslatef(0, 0, 0.06f);
            GLU.gluDisk(quad, 0, 0.18f, 20, 1);
            GL.glPushMatrix();
            GL.glTranslatef(-0.04f, -0.04f, 0);
            DrawBox(0.08f, 0.08f, 0.30f);
            GL.glPopMatrix();
            GL.glPushMatrix();
            GL.glTranslatef(-0.14f, -0.04f, 0.16f);
            DrawBox(0.28f, 0.08f, 0.08f);
            GL.glPopMatrix();
            GL.glPopMatrix();
            GL.glEndList();
        }

        static void DrawBox(float w, float d, float h)
        {
            float x1 = w, y1 = d, z1 = h;
            GL.glBegin(GL.GL_QUADS);
            GL.glNormal3f(0, 0, -1);
            GL.glVertex3f(0, 0, 0); GL.glVertex3f(x1, 0, 0);
            GL.glVertex3f(x1, y1, 0); GL.glVertex3f(0, y1, 0);
            GL.glNormal3f(0, 0, 1);
            GL.glVertex3f(0, 0, z1); GL.glVertex3f(x1, 0, z1);
            GL.glVertex3f(x1, y1, z1); GL.glVertex3f(0, y1, z1);
            GL.glNormal3f(0, -1, 0);
            GL.glVertex3f(0, 0, 0); GL.glVertex3f(x1, 0, 0);
            GL.glVertex3f(x1, 0, z1); GL.glVertex3f(0, 0, z1);
            GL.glNormal3f(0, 1, 0);
            GL.glVertex3f(0, y1, 0); GL.glVertex3f(x1, y1, 0);
            GL.glVertex3f(x1, y1, z1); GL.glVertex3f(0, y1, z1);
            GL.glNormal3f(-1, 0, 0);
            GL.glVertex3f(0, 0, 0); GL.glVertex3f(0, y1, 0);
            GL.glVertex3f(0, y1, z1); GL.glVertex3f(0, 0, z1);
            GL.glNormal3f(1, 0, 0);
            GL.glVertex3f(x1, 0, 0); GL.glVertex3f(x1, y1, 0);
            GL.glVertex3f(x1, y1, z1); GL.glVertex3f(x1, 0, z1);
            GL.glEnd();
        }
    }
}