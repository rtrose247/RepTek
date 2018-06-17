using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Graphics;
using Secret_Hipster.OpenGLPrograms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Secret_Hipster.Primitives
{
    public class Grid : IDrawable
    {
        private Buffer verticesBuffer;
        private Buffer colorBuffer;
        private Matrix4 modelMatrix;

        public Grid(Color color)
        {
            this.Init(color);
        }

        private void Init(Color color)
        {
            this.modelMatrix = Matrix4.Identity;
            this.verticesBuffer = new Buffer(this.GenerateVertices().ToArray());

            var colordata = ColorData(color).ToArray();
            this.colorBuffer = new Buffer(colordata);
        }

        //10=>100
        private IEnumerable<Vector3> GenerateVertices()
        {
            for (int i = -100; i < 100; i++)
            {
                yield return new Vector3(-100, 0, i);
                yield return new Vector3(100, 0, i);
            }

            for (int i = -100; i < 100; i++)
            {
                yield return new Vector3(i, 0, -100);
                yield return new Vector3(i, 0, 100);
            }

            yield return new Vector3(-100, 0, 100);
            yield return new Vector3(100, 0, 100);

            yield return new Vector3(100, 0, -100);
            yield return new Vector3(100, 0, 100);
        }

        private IEnumerable<Vector3> ColorData(Color color)
        {
            for (int i = 0; i < this.verticesBuffer.Length; i++)
            {
                yield return new Vector3(color.R, color.G, color.B);
            }
        }

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