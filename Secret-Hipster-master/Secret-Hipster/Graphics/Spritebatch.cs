using OpenTK.Graphics.OpenGL;
using Secret_Hipster.OpenGLPrograms;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Secret_Hipster.Graphics
{
    public class Spritebatch
    {
        public Camera Camera { get; private set; }
        public TextureProgram TextureProgram { get; private set; }
        public ColorProgram ColorProgram { get; private set; }

        private bool hasBegun;
        private Type programType;

        public Spritebatch(Camera camera)
        {
            this.Camera = camera;
            this.Init();
        }

        private void Init()
        {
            this.TextureProgram = new TextureProgram();
            this.ColorProgram = new ColorProgram();
            this.hasBegun = false;
        }

        public void Begin<T>() where T : BaseProgram
        {
            if (hasBegun)
            {
                throw new Exception("You should not call the begin method twice");
            }
            this.programType = typeof(T);

            if (programType == typeof(TextureProgram))
            {
                this.TextureProgram.UseProgram();
            }
            if (programType == typeof(ColorProgram))
            {
                this.ColorProgram.UseProgram();
            }

            hasBegun = true;
        }

        public void End()
        {
            if (!hasBegun)
            {
                throw new Exception("Must call begin before you call end");
            }

            if (programType == typeof(TextureProgram))
            {
                this.TextureProgram.EndProgram();
            }
            if (programType == typeof(ColorProgram))
            {
                this.ColorProgram.EndProgram();
            }

            GL.Flush();
            hasBegun = false;
        }

        public static void GenerateTexture(Bitmap image, out int textureId)
        {
            textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                data.Width,
                data.Height,
                0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0);

            image.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            // Dispose and release texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
            image.Dispose();
        }
    }
}