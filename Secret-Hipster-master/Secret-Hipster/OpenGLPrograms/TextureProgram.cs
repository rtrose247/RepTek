using OpenTK.Graphics.OpenGL;
using System;

namespace Secret_Hipster.OpenGLPrograms
{
    public class TextureProgram : BaseProgram
    {
        public int PositionAttribute { get; private set; }
        public int TexturePointsAttribute { get; private set; }
        public int ModelViewUniform { get; private set; }
        public int TextureUniform { get; private set; }

        private readonly int program;

        public TextureProgram()
        {
            this.program = GL.CreateProgram();
            this.LoadShaders("fs", ShaderType.FragmentShader, this.program);
            this.LoadShaders("vs", ShaderType.VertexShader, this.program);

            GL.LinkProgram(this.program);
            this.AttributeMapping();
        }

        private void AttributeMapping()
        {
            this.PositionAttribute = GL.GetAttribLocation(this.program, "vPosition");
            this.TexturePointsAttribute = GL.GetAttribLocation(this.program, "vTexturePoint");
            this.ModelViewUniform = GL.GetUniformLocation(this.program, "modelview");
            this.TextureUniform = GL.GetUniformLocation(this.program, "texUnit");

#if DEBUG
            if (this.PositionAttribute == -1 ||
                this.TexturePointsAttribute == -1 ||
                this.ModelViewUniform == -1)
            {
                Console.WriteLine("Error binding attributes");
            }
#endif
        }

        public override void UseProgram()
        {
            GL.UseProgram(this.program);

            GL.EnableVertexAttribArray(this.PositionAttribute);
            GL.EnableVertexAttribArray(this.TexturePointsAttribute);
        }

        public override void EndProgram()
        {
            GL.DisableVertexAttribArray(PositionAttribute);
            GL.DisableVertexAttribArray(TexturePointsAttribute);
        }
    }
}