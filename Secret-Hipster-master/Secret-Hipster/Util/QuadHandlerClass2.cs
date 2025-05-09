﻿using OpenTK;
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
    public class QuadHandlerClass2
    {
        public List<TextureQuad> TextureQuads { get; set; }
        //additional container for member data
        //=> bitmap array
        //public Bitmap[] myBitmaps;

        //=> texture ID array
        public int[] myTextures;
        public int myTextureIndex;

        public QuadHandlerClass2()
        {
            this.Init();
        }

        public void Init()
        {
            this.TextureQuads = new List<TextureQuad>();
            this.CreateTexture();
            this.CreateCubes();
        }

        //Overrides
        //I's => interiors
        public void CreateTexture()
        {
            //Begin with "i" scenes
            //
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            //
            //
            //var ii = Environment.CurrentDirectory.LastIndexOf(@"\");
            //var path = Environment.CurrentDirectory.Substring(0, ii);
            //
            //i=>interiors
            //String myDirectory = (System.IO.Path.Combine(dir, "Textures\\i\\256")).ToString();
            //
            //e=>exteriors
            //String myDirectory = (System.IO.Path.Combine(dir, "Textures\\e\\256")).ToString();
            //
            //r=>runway|actually "h" for (h)umanoid models
            //String myDirectory = (System.IO.Path.Combine(dir, "Textures\\r\\256")).ToString();

            //brg
            //String myDirectory = (System.IO.Path.Combine(dir, "..\\..\\Textures\\brg\\256")).ToString();

            //city night
            //String myDirectory = (System.IO.Path.Combine(dir, "..\\..\\..\\Secret-Hipster-master\\Secret-Hipster\\Textures\\c.night\\256")).ToString();

            //honolulu night
            String myDirectory = (System.IO.Path.Combine(dir, "..\\..\\..\\Secret-Hipster-master\\Secret-Hipster\\Textures\\h.night\\256")).ToString();

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

        public void CreateCubes()
        {
            //simple(!)
            //use tex counter to loop through all images
            //use wrap-around
            int myTexCounter = 0;

            double ii = 0;
            double jj = 0;
            //elevation
            int y = 0;
            int ystep = 1;

            //tmp auto-layout
            for (y = 0; y < 6; y++)
            { 
                for (int i = -10; i < 10; i++)
                {
                    for (int j = -10; j < 10; j++)
                    {
                        TextureQuad cube;
                        //
                        //possible fork
                        if (i % 2 == 0)
                        {
                            cube = new TextureQuad(this.myTextures[myTexCounter]);
                            cube.Position = new Vector3(2 * i, 2 * y + 1, 2 * j);
                            cube.StartAfterSeconds = ii + jj;
                        }
                        else
                        {
                            cube = new TextureQuad(this.myTextures[myTexCounter]);
                            float alty = 0.50f * ystep;
                            cube.Position = new Vector3(3 * i, alty + 1, 3 * j);
                            cube.StartAfterSeconds = ii + jj;
                        }

                        this.TextureQuads.Add(cube);

                        jj += 0.5;

                        //maybe
                        myTexCounter++;
                        if (myTexCounter >= myTextureIndex)
                            myTexCounter = 0;

                    }
                    //
                    jj = 0;
                    ii += 2.0;
                    //
                    ystep++;
                }

            }//end for(s)
        }

        public void Update(double time)
        {
            foreach (var textureQuad in TextureQuads)
            {
                textureQuad.Update(time);
            }
        }

        public void Draw(Spritebatch spritebatch)
        {
            for (int i = 0; i < TextureQuads.Count; i++)
            {
                TextureQuads[i].Draw(spritebatch);
            }
        }

    }
}
