using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Graphics;
using Secret_Hipster.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Secret_Hipster.Util
{
    //RTR 6/16/2018
    //exp. with fixed layout(s)
    //
    class QuadHandlerClass2 : QuadHandler
    {
        //additional container for member data
        //=> bitmap array
        //public Bitmap[] myBitmaps;

        //=> texture ID array
        public int[] myTextures;
        public int myTextureIndex;

        //Overrides
        //I's => interiors
        public override void CreateTexture()
        {
            //Begin with "i" scenes
            //
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            //
            //
            //var ii = Environment.CurrentDirectory.LastIndexOf(@"\");
            //var path = Environment.CurrentDirectory.Substring(0, ii);
            //
            //
            String myDirectory = (System.IO.Path.Combine(dir, "Textures\\i\\256")).ToString();
            // Get list of images
            //
            //DirectoryInfo d = new DirectoryInfo(@"D:\Test");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            //string str = "";
            //foreach (FileInfo file in Files)
            //{
            //    str = str + ", " + file.Name;
            //}
            DirectoryInfo d = new DirectoryInfo(myDirectory);
            FileInfo[] Files = d.GetFiles();

            //allocate texture array
            myTextures = new int[Files.Length];
            
            //count loaded textures
            myTextureIndex = 0;
            foreach (FileInfo file in Files)
            {
                var bitmap = new Bitmap(file.FullName);
                int texID;
                Spritebatch.GenerateTexture(bitmap, out texID);
                myTextures[myTextureIndex++] = texID;

            }

        }

        public override void CreateCubes()
        {
            //simple(!)
            //use tex counter to loop through all images
            //use wrap-around
            int myTexCounter = 0;

            double ii = 0;
            double jj = 0;
            //elevation
            int y = 0;

            for (y = 0; y < 99; y++)
            { 
                for (int i = -3; i < 4; i++)
                {
                    for (int j = -3; j < 4; j++)
                    {
                        TextureQuad cube;
                        //
                        //possible fork
                        if (i % 2 == 0)
                        {
                            cube = new TextureQuad(this.myTextures[myTexCounter]);
                            cube.Position = new Vector3(3 * i, 3 * y + 1, 3 * j);
                            cube.StartAfterSeconds = ii + jj;
                        }
                        else
                        {
                            cube = new TextureQuad(this.myTextures[myTexCounter]);
                            cube.Position = new Vector3(3 * i, 3 * y + 1, 3 * j);
                            cube.StartAfterSeconds = ii + jj;
                        }

                        this.TextureQuads.Add(cube);

                        jj += 0.5;

                        //texture counter
                        //
                        myTexCounter++;
                        if (myTexCounter >= myTextureIndex)
                            myTexCounter = 0;
                    }
                    jj = 0;
                    ii += 0.5;
                }

            }//end for(s)
        }

        public override void Draw(Spritebatch spritebatch)
        {
            for (int i = 0; i < TextureQuads.Count; i++)
            {
                TextureQuads[i].Draw(spritebatch);
            }
        }

    }
}
