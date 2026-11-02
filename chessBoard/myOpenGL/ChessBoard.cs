using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    class ChessBoard
    {
        const float SQUARE_SIZE = 1.0f;
        const int BOARD_SIZE = 8;
        const float BOARD_Y = 0.0f;
        const float BORDER = 0.3f;

        uint textureID = 0;

        public void Init()
        {
            GenerateCheckerTexture();
        }

        public void Draw()
        {
            DrawBorder();
            DrawSquares();
        }

        void GenerateCheckerTexture()
        {
            byte[] pixels = new byte[]
            {
                255, 255, 220, 255,    // top-left:     cream (light square)
                  0,   0,   0, 255,    // top-right:    black (dark square)
                  0,   0,   0, 255,    // bottom-left:  black (dark square)
                255, 255, 220, 255,    // bottom-right: cream (light square)
            };

            uint[] ids = new uint[1];
            GL.glGenTextures(1, ids);
            textureID = ids[0];

            GL.glBindTexture(GL.GL_TEXTURE_2D, textureID);

            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_NEAREST);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_NEAREST);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);

            // pin the byte array so OpenGL can safely read it
            GCHandle handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGBA,
                                2, 2, 0,
                                GL.GL_RGBA, GL.GL_UNSIGNED_byte,
                                handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        void DrawSquares()
        {
            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, textureID);
            GL.glColor3f(1f, 1f, 1f);
            GL.glNormal3f(0f, 1f, 0f);

            float half = BOARD_SIZE * SQUARE_SIZE / 2f;

            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0f, 0f); GL.glVertex3f(-half, BOARD_Y, -half);
            GL.glTexCoord2f(4f, 0f); GL.glVertex3f(half, BOARD_Y, -half);
            GL.glTexCoord2f(4f, 4f); GL.glVertex3f(half, BOARD_Y, half);
            GL.glTexCoord2f(0f, 4f); GL.glVertex3f(-half, BOARD_Y, half);
            GL.glEnd();

            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        void DrawBorder()
        {
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glColor3f(0.55f, 0.27f, 0.07f);

            float inner = BOARD_SIZE * SQUARE_SIZE / 2f;  // 4.0
            float outer = inner + BORDER;                  // 4.3

            DrawFlatQuad(-outer, BOARD_Y, -outer, outer, BOARD_Y, -inner);
            DrawFlatQuad(-outer, BOARD_Y, inner, outer, BOARD_Y, outer);
            DrawFlatQuad(-outer, BOARD_Y, -inner, -inner, BOARD_Y, inner);
            DrawFlatQuad(inner, BOARD_Y, -inner, outer, BOARD_Y, inner);

            GL.glEnable(GL.GL_LIGHTING);
        }

        void DrawFlatQuad(float x0, float y, float z0, float x1, float y1, float z1)
        {
            GL.glBegin(GL.GL_QUADS);
            GL.glNormal3f(0f, 1f, 0f);
            GL.glVertex3f(x0, y, z0);
            GL.glVertex3f(x1, y, z0);
            GL.glVertex3f(x1, y1, z1);
            GL.glVertex3f(x0, y1, z1);
            GL.glEnd();
        }

        public static float ColToX(int col) { return -3.5f + col * SQUARE_SIZE; }
        public static float RowToZ(int row) { return -3.5f + row * SQUARE_SIZE; }
    }
}