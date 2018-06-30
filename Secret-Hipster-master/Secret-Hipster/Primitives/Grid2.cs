using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Graphics;
using Secret_Hipster.OpenGLPrograms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Secret_Hipster.Primitives
{
    public class Grid2 : IDrawable
    {
        public Graphics.Buffer verticesBuffer;
        public Graphics.Buffer colorBuffer;
        public Matrix4 modelMatrix;

        public Grid2(Color color)
        {
            this.Init2(color);
        }

        private void Init2(Color color)
        {
            this.modelMatrix = Matrix4.Identity;
            this.verticesBuffer = new Graphics.Buffer(this.GenerateVertices2().ToArray());

            var colordata = ColorData(color).ToArray();
            this.colorBuffer = new Graphics.Buffer(colordata);
        }

        //10=>100
        public IEnumerable<Vector3> GenerateVertices2()
        {
            //simple(!)
            //use tex counter to loop through all images
            //use wrap-around
            //int myTexCounter = 0;

            double ii = 0;
            double jj = 0;
            //elevation
            int y = 0;
            int ystep = 1;

            for (y = 0; y < 16; y++)
            {
                for (int i = -60; i < 60; i++)
                {
                    for (int j = -60; j < 60; j++)
                    {
                        //
                        //possible fork
                        if (i % 2 == 0)
                        {
                            yield return new Vector3(i, 6 * y + 1, j);
                            yield return new Vector3(i + 1, 6 * y + 1, j);
                            yield return new Vector3(i + 1, 6 * y + 1, j + 1);
                            yield return new Vector3(i, 6 * y + 1, j + 1);
                            //yield return new Vector3(i, 6 * y + 1, j);
                        }
                        //else
                        //{
                            //float alty = 0.50f * ystep;
                            //yield return new Vector3(i, alty + 1, j);
                            //yield return new Vector3(i + 1, alty + 1, j + 1);
                            //yield return new Vector3(i, alty + 1, j + 1);
                            //yield return new Vector3(i + 1, alty + 1, j);
                        //}
                        jj += 0.5;
                    }
                    //
                    jj = 0;
                    ii += 0.5;
                    //
                    ystep++;
                }

            }//end for
        }//end generate vertices 2

        //
        public IEnumerable<Vector3> ColorData(Color color)
        {
            for (int i = 0; i < this.verticesBuffer.Length; i++)
            {
                yield return new Vector3(color.R, color.G, color.B);
            }
        }
        //
        public void Draw(Spritebatch spritebatch)
        {
            spritebatch.Begin<ColorProgram>();

            // Matrix transformation
            Matrix4 transformationMatrix = modelMatrix * spritebatch.Camera.GetViewProjectionMatrix();
            GL.UniformMatrix4(spritebatch.ColorProgram.ModelViewUniform, false, ref transformationMatrix);

            // Attribute pointers
            GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBuffer.MemoryLocation);
            GL.VertexAttribPointer(spritebatch.ColorProgram.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer.MemoryLocation);
            GL.VertexAttribPointer(spritebatch.ColorProgram.ColorAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.DrawArrays(PrimitiveType.Lines, 0, verticesBuffer.Length);

            spritebatch.End();
        }
    }
}
