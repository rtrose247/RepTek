using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.OpenGLPrograms;
using System.Collections.Generic;
using System.Drawing;

namespace Secret_Hipster.Graphics
{
    public class CustomRectangle
    {
        public Vector3 Position { get; set; }
        public float Size { get; set; }
    }

    public class SierpinskiCarpet : IDrawable
    {
        private List<CustomRectangle> rectangles;
        private Buffer verticesBuffer;
        private Buffer colorBuffer;

        public SierpinskiCarpet(int size)
        {
            this.verticesBuffer = new Buffer(Primitives.CubeVertices);
            this.colorBuffer = new Buffer(this.GenerateColorVectors(Color.Black, verticesBuffer.Length));
            rectangles = new List<CustomRectangle>();

            this.CreateSierpinskiCarpet(0, 0, 0, size);
        }

        private void CreateSierpinskiCarpet(float x, float y, float z, float side)
        {
            var size = side / (float)3;

            rectangles.Add(new CustomRectangle { Size = size, Position = new Vector3 { X = x + size + size / 2, Y = y + size + size / 2, Z = z + size + size / 2 } });

            

            if (size >= 0.2f)
            {
                //CreateSierpinskiCarpet(x, y, z, size);
                //CreateSierpinskiCarpet(x + size, y, z, size);
                //CreateSierpinskiCarpet(x + 2 * size, y, z, size);
                //CreateSierpinskiCarpet(x, y + size, z, size);
                //CreateSierpinskiCarpet(x + 2 * size, y + size, z, size);
                //CreateSierpinskiCarpet(x, y + 2 * size, z, size);
                //CreateSierpinskiCarpet(x + size, y + 2 * size, z, size);
                //CreateSierpinskiCarpet(x + 2 * size, y + 2 * size, z, size);


                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            if ((Math.Abs(i) + Math.Abs(j) + Math.Abs(k)) > 1)
                            {
                                CreateSierpinskiCarpet(x + (i * size), y + (j * size), z + (k * size), size);
                            }
                        }
                    }
                }
            }
        }

        private Vector3[] GenerateColorVectors(Color color, int verticesLenght)
        {
            var list = new List<Vector3>();
            for (int i = 0; i < verticesLenght; i++)
            {
                list.Add(new Vector3 { X = color.R, Y = color.G, Z = color.B });
            }

            return list.ToArray();
        }

        public void Draw(Spritebatch spritebatch)
        {
            spritebatch.Begin<ColorProgram>();

            foreach (var rectangle in rectangles)
            {
                // Matrix transformation           
                Matrix4 transformationMatrix = Matrix4.CreateScale(0.2f) * Matrix4.CreateTranslation(rectangle.Position.X, rectangle.Position.Y, rectangle.Position.Z) * spritebatch.Camera.GetViewProjectionMatrix();
                GL.UniformMatrix4(spritebatch.TextureProgram.ModelViewUniform, false, ref transformationMatrix);

                // Attribute pointers
                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBuffer.MemoryLocation);
                GL.VertexAttribPointer(spritebatch.ColorProgram.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer.MemoryLocation);
                GL.VertexAttribPointer(spritebatch.ColorProgram.ColorAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.DrawArrays(PrimitiveType.Quads, 0, verticesBuffer.Length);


                //Matrix4 sfasf = Matrix4.CreateScale(0.33f) * Matrix4.CreateTranslation(rectangle.Position.X, rectangle.Position.Y, 2) * spritebatch.Camera.GetViewProjectionMatrix();
                //GL.UniformMatrix4(spritebatch.TextureProgram.ModelViewUniform, false, ref sfasf);

                //// Attribute pointers
                //GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBuffer.MemoryLocation);
                //GL.VertexAttribPointer(spritebatch.ColorProgram.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

                //GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer.MemoryLocation);
                //GL.VertexAttribPointer(spritebatch.ColorProgram.ColorAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

                //GL.DrawArrays(PrimitiveType.Quads, 0, verticesBuffer.Length);
            }

            spritebatch.End();
        }
    }
}