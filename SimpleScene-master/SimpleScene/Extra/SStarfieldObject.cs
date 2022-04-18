﻿using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

using SimpleScene;

namespace SimpleScene.Demos
{

	// TODO: this would be more attractive with some varied star-textures

	public class SStarfieldObject : SSObject
	{
		SSVertex_PosNormDiff[] vertices = null;
		int numstars;

        public override bool alphaBlendingEnabled { get { return true; } }

		public SStarfieldObject (int numstars) 
        {
            this.renderState.lighted = false;
            this.renderState.depthTest = true;
            this.renderState.depthWrite = false;

		    // generate the stars
		    this.numstars = numstars;
		    vertices = new SSVertex_PosNormDiff[numstars];


			Random rgen = new Random();

			for (int i = 0; i < numstars; i++) {
                // generate a random position
                vertices[i].Position = new Vector3(
                    (float)rgen.NextDouble() * 2.0f - 1.0f,
                    (float)rgen.NextDouble() * 2.0f - 1.0f,
                    (float)rgen.NextDouble() * 2.0f - 1.0f);

                // the normals are simplified
                vertices[i].Normal = vertices[i].Position;
                int intensity = rgen.Next(80);
                if (intensity > 70) { intensity += 20 + rgen.Next(50); }
                // TODO: add a couple dominant standout colors (red giant, yellow), maybe a star-color-palette
                vertices[i].DiffuseColor = Color.FromArgb(255,
                    rgen.Next(40) + intensity,
                    rgen.Next(25) + intensity,
                    rgen.Next(25)+ intensity).ToArgb();
            }
		}

		public override void Render(SSRenderConfig renderConfig) 
        {
            base.Render(renderConfig);
			SSShaderProgram.DeactivateAll ();

			GL.Disable(EnableCap.Texture2D);
			GL.Disable(EnableCap.Lighting);	

			GL.Enable(EnableCap.PointSmooth);

			GL.PointSize(1.5f);
			GL.Begin(BeginMode.Points);
			for (int i = 0; i < this.numstars; i++) {			   

			   GL.Color3(Color.FromArgb(vertices[i].DiffuseColor));
			   GL.Normal3(vertices[i].Normal);
			   GL.Vertex3(vertices[i].Position);
			}
			GL.End();
		}	
	}
}

