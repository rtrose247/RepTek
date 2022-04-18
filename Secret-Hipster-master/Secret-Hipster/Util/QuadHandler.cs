using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Graphics;
using Secret_Hipster.Primitives;
using System.Collections.Generic;
using System.Drawing;

namespace Secret_Hipster.Util
{
    public class QuadHandler
    {
        public List<TextureQuad> TextureQuads { get; set; }
        public int greenTexture;
        public int blueTexture;

        public QuadHandler()
        {
            this.Init();
        }

        public void Init()
        {
            this.TextureQuads = new List<TextureQuad>();
            this.CreateTexture();
            this.CreateCubes();
        }

        public virtual void CreateTexture()
        {
            var bitmap = new Bitmap("Textures/Containers/container001-blue.png");
            Spritebatch.GenerateTexture(bitmap, out this.blueTexture);

            var bitmap2 = new Bitmap("Textures/Containers/container001-green.png");
            Spritebatch.GenerateTexture(bitmap2, out this.greenTexture);
        }

        public virtual void CreateCubes()
        {
            double ii = 0;
            double jj = 0;

            for (int i = -3; i < 4; i++)
            {
                for (int j = -3; j < 4; j++)
                {
                    TextureQuad cube;

                    if (i % 2 == 0)
                    {
                        cube = new TextureQuad(this.blueTexture);
                        cube.Position = new Vector3(3 * i, 1, 3 * j);
                        cube.StartAfterSeconds = ii + jj;
                    }
                    else
                    {
                        cube = new TextureQuad(this.greenTexture);
                        cube.Position = new Vector3(3 * i, 1, 3 * j);
                        cube.StartAfterSeconds = ii + jj;
                    }

                    this.TextureQuads.Add(cube);

                    jj += 0.5;
                }
                jj = 0;
                ii += 0.5;
            }
        }
        
        public virtual void Update(double time)
        {
            foreach (var textureQuad in TextureQuads)
            {
                textureQuad.Update(time);
            }
        }

        public virtual void Draw(Spritebatch spritebatch)
        {
            for (int i = 0; i < TextureQuads.Count; i++)
            {
                TextureQuads[i].Draw(spritebatch);
            }
        }
    }
}
