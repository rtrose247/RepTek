using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using Secret_Hipster.Extensions;
using Secret_Hipster.Graphics;

using Buffer = Secret_Hipster.Graphics.Buffer;

namespace Secret_Hipster.Primitives
{
    public class TextureQuad : IDrawable
    {
        private static Buffer verticesBuffer;
        private static Buffer texturePointBuffer;
        public static Buffer VerticesBuffer { get { return verticesBuffer ?? (verticesBuffer = new Buffer(Graphics.Primitives.CubeVertices)); } }
        public static Buffer TexturePointBuffer { get { return texturePointBuffer ?? (texturePointBuffer = new Buffer(Graphics.Primitives.CubeTexturePoints)); } }

        private Vector3 position;
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                this.position = value;
                this.target = this.position;
                this.target.Y += 3;
            }
        }

        public double StartAfterSeconds { get; set; }

        public int Texture { get; private set; }

        private Matrix4 modelMatrix;
        private float rotation;

        // Moving vectors
        private Vector3 velocity;
        private Vector3 target;
        private Vector3 steering;

        private float slowingRadius = 5f;
        private float maxVelocity = 0.07f;

        private float maxForce = 0.3f;

        public TextureQuad(int texture)
        {            
            this.Position = Vector3.Zero;
            this.Texture = texture;

            //StartAfterSeconds = 3;
            this.velocity = Vector3.Zero;
        }

        public void Update(double time)
        {
            //rotation += (float)time;            
            ////this.modelMatrix = Matrix4.CreateTranslation(0, 0, 1f) * Matrix4.CreateRotationY(rotation) * Matrix4.CreateTranslation(Position);

            ////this.velocity = Vector3.Normalize(target - this.Position) * this.maxVelocity;
            
            //if (StartAfterSeconds <= 0)
            //{
            //    var desiredVelocity = this.target - this.position;
            //    var distance = desiredVelocity.Length;

            //    if (distance < this.slowingRadius)
            //    {
            //        desiredVelocity = Vector3.Normalize(desiredVelocity) * maxVelocity * (distance / slowingRadius);

            //        if (distance < 0.7f)
            //        {
            //            this.target.Y = target.Y * -1;
            //        }
            //    }
            //    else
            //    {
            //        desiredVelocity = Vector3.Normalize(desiredVelocity) * maxVelocity;
            //    }
            //    this.steering += desiredVelocity - velocity;

            //    this.steering = steering.Truncate(maxForce);
            //    this.steering = steering / 20;
            //    this.velocity = (velocity + steering).Truncate(this.maxVelocity);
            //    this.position += this.velocity;
            //}
            //else
            //{
            //    StartAfterSeconds -= time;
            //}


            this.modelMatrix = Matrix4.CreateTranslation(Position);
        }

        public void Draw(Spritebatch spritebatch)
        {
            // Matrix transformation           
            Matrix4 transformationMatrix = modelMatrix * spritebatch.Camera.GetViewProjectionMatrix();
            GL.UniformMatrix4(spritebatch.TextureProgram.ModelViewUniform, false, ref transformationMatrix);

            // Use texture
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, this.Texture);
            GL.Uniform1(spritebatch.TextureProgram.TextureUniform, (int)TextureUnit.Texture0);

            // Attribute pointers
            GL.BindBuffer(BufferTarget.ArrayBuffer, VerticesBuffer.MemoryLocation);
            GL.VertexAttribPointer(spritebatch.TextureProgram.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TexturePointBuffer.MemoryLocation);
            GL.VertexAttribPointer(spritebatch.TextureProgram.TexturePointsAttribute, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.DrawArrays(PrimitiveType.Quads, 0, VerticesBuffer.Length);
        }
    }
}
