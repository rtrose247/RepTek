using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace Secret_Hipster.OpenGLPrograms
{
    public abstract class BaseProgram
    {
        private const string ShaderFolder = "Shaders";

        /// <summary>
        /// Will load shader to program and compile it
        /// </summary>
        /// <param name="shader"></param>
        /// <param name="type"></param>
        /// <param name="program"></param>
        /// <param name="address"></param>
        protected void LoadShaders(string shader, ShaderType type, int program)
        {
            int address = GL.CreateShader(type);
            using (var sr = new StreamReader(string.Format("{0}/{1}.glsl", ShaderFolder, shader)))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);

#if DEBUG
            // Compilation information 
            Console.WriteLine(GL.GetShaderInfoLog(address));
#endif
        }

        public abstract void UseProgram();
        public abstract void EndProgram();
    }
}