using OpenTK.Graphics.OpenGL;
using System;

namespace Secret_Hipster.OpenGLPrograms
{
    public class ColorProgram : BaseProgram
    {
        public int PositionAttribute { get; private set; }
        public int ColorAttribute { get; private set; }
        public int ModelViewUniform { get; private set; }

        private readonly int program;

        public ColorProgram()
        {
            this.program = GL.CreateProgram();
            this.LoadShaders("Default/fs", ShaderType.FragmentShader, this.program);
            this.LoadShaders("Default/vs", ShaderType.VertexShader, this.program);

            GL.LinkProgram(this.program);
            this.AttributeMapping();
        }

        private void AttributeMapping()
        {
            this.PositionAttribute = GL.GetAttribLocation(this.program, "vPosition");
            this.ColorAttribute = GL.GetAttribLocation(this.program, "vColor");
            this.ModelViewUniform = GL.GetUniformLocation(this.program, "modelview");

#if DEBUG
            if (this.PositionAttribute == -1 ||
                this.ColorAttribute == -1 ||
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
            GL.EnableVertexAttribArray(this.ColorAttribute);
        }

        public override void EndProgram()
        {
            GL.DisableVertexAttribArray(PositionAttribute);
            GL.DisableVertexAttribArray(ColorAttribute);
        }
    }
}