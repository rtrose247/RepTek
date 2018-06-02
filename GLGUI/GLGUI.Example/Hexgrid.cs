using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
//
using Assimp.Configs;
using Assimp;

namespace GLGUI.Example
{
    public struct Position
    {
        public float x;
        public float y;
        public float z;

        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }



    public class Hexgrid
    {
        //from "plaza"+"asteroid"
        static int list;
        static Random r = new Random();
        public Position p;
        //int initList;
        int dlHexagon;
        int dlWorld;

        private float SQRT_3;
        private int zline;
        private int indent;
        private float sq3z;
        private float i6x;
        //


        public void Create()
        {
            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);

            dlHexagon = GL.GenLists(1);
            GL.NewList(dlHexagon, ListMode.Compile);

            SQRT_3 = 1.7320F; // 508075688772935274463415059;

            GL.Disable(EnableCap.Texture2D);
            //Gl.glColor3f(1.0f, 1.0f, 0.0f);
            //Gl.glColor3f(0.0f, 1.0f, 0.0f); 
            //
            //Temp hard code yellow
            //
            GL.Enable(EnableCap.ColorArray);
            GL.Color3(1.0f, 1.0f, 0.0f);
            //
            GL.Begin(mode: BeginMode.Quads);
            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(-1.0f, 0.0f, -SQRT_3);
            GL.Vertex3(1.0f, 0.0f, -SQRT_3);
            GL.Vertex3(1.0f, 1.0f, -SQRT_3);
            GL.Vertex3(-1.0f, 1.0f, -SQRT_3);
            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(-1.0f, 0.0f, SQRT_3);
            GL.Vertex3(1.0f, 0.0f, SQRT_3);
            GL.Vertex3(1.0f, 1.0f, SQRT_3);
            GL.Vertex3(-1.0f, 1.0f, SQRT_3);
            GL.Normal3(0.8660254f, 0.0f, 0.5f);
            GL.Vertex3(-1.0f, 0.0f, -SQRT_3);
            GL.Vertex3(-1.0f, 1.0f, -SQRT_3);
            GL.Vertex3(-2.0f, 1.0f, 0.0f);
            GL.Vertex3(-2.0f, 0.0f, 0.0f);
            GL.Normal3(-0.8660254f, 0.0f, 0.5f);
            GL.Vertex3(1.0f, 0.0f, -SQRT_3);
            GL.Vertex3(1.0f, 1.0f, -SQRT_3);
            GL.Vertex3(2.0f, 1.0f, 0.0f);
            GL.Vertex3(2.0f, 0.0f, 0.0f);
            GL.Normal3(0.8660254f, 0.0f, -0.5f);
            GL.Vertex3(-1.0f, 0.0f, SQRT_3);
            GL.Vertex3(-1.0f, 1.0f, SQRT_3);
            GL.Vertex3(-2.0f, 1.0f, 0.0f);
            GL.Vertex3(-2.0f, 0.0f, 0.0f);
            GL.Normal3(-0.8660254f, 0.0f, -0.5f);
            GL.Vertex3(1.0f, 0.0f, SQRT_3);
            GL.Vertex3(1.0f, 1.0f, SQRT_3);
            GL.Vertex3(2.0f, 1.0f, 0.0f);
            GL.Vertex3(2.0f, 0.0f, 0.0f);
            GL.End();
            GL.EndList();

            //<=
            dlWorld = GL.GenLists(1);
            GL.NewList(dlWorld, ListMode.Compile);
            GL.Disable(EnableCap.Texture2D);
            //
            GL.Enable(EnableCap.ColorArray);
            GL.Color3(1.0f, 1.0f, 0.0f);
            //
            zline = 0;

            for (int z = 0; z < 58; z++)
            {
                zline += 1;
                if ((zline % 2) > 0)
                    indent = 3; //3.0;
                else
                    indent = 0;
                GL.PushMatrix();
                //
                sq3z = (SQRT_3 + 0.01f) * z;//((SQRT_3 + 0.01) * z);
                GL.Translate(0.0f, 0.0f, sq3z);

                for (int x = 1; x < 17; x++)
                {
                    //float yy = z * 0.1f;
                    GL.PushMatrix();
                    i6x = 6 * x + indent;
                    GL.Translate(i6x, 0.0f, 0.0f); //0.0f, 0.0f);
                    GL.CallList(dlHexagon);
                    GL.PopMatrix();
                }
                GL.PopMatrix();

            }

            //Gl.glBegin(Gl.GL_QUADS);
            //GL.Normal3(0, 1, 0);
            //Gl.glVertex3i(0, 0, 0);
            //Gl.glVertex3i(0, 0, 100);
            //Gl.glVertex3i(100, 0, 100);
            //Gl.glVertex3i(100, 0, 0);
            //Gl.glEnd();
            GL.Begin(mode: BeginMode.Lines);
            GL.Color3(1.0f, 1.0f, 1.0f); // White (RGB)
            for (float x = 0; x < 108.0f; x += 1.0f)
            {
                GL.Vertex3(x, 0.0f, 0.0f);
                GL.Vertex3(x, 0.0f, (98.0f));
            }
            for (float z = 0; z < 98; z += 1.0f)
            {
                GL.Vertex3(0.0f, 0.0f, z);
                GL.Vertex3((108.0f), 0.0f, z);
            }
            GL.End();


            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Enable(EnableCap.Texture2D);
            GL.EndList();
        }

        public void Draw2()
        {
            //p.z += this.asteroidSpeed * 0.01f * 1.0f;
            //            p.y += incY;
            //            p.x += incX;
            //            rot += 1f;
            //tmp
            //
            GL.PushMatrix();
            GL.Translate(p.x, p.y, p.z);
            GL.CallList(dlWorld);
            GL.PopMatrix();
            //

        }
    }
}
