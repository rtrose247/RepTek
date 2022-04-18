﻿using System;
using System.Collections.Generic;
using SimpleScene;
using SimpleScene.Util;
using SimpleScene.Demos;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TestBench2
{
	public class TestBench2 : TestBenchBootstrap
	{
		protected Random rand = new Random();

		/// <summary>
		/// For rendering (transparent) occlusion test disks that are used in the flare intensity mechanic
		/// </summary>
		protected SSScene laserOccDiskScene3d;

		/// <summary>
		/// For rendering laser 2d effects
		/// </summary>
		protected SSScene laserFlareScene2d;

		protected SLaserManager laserManager = null;

		//protected SimpleLaserParameters laserParams = null;
		protected WeakReference activeLaser = new WeakReference(null);

		protected SSObjectMesh droneObj1;
		protected Matrix4 laserSourceTxfm;

		protected SSObjectMesh droneObj2;

		public TestBench2()
			: base("TestBench2: Lasers")
		{
			shadowmapDebugQuad.renderState.visible = false;
		}

		static void Main()
		{
			// The 'using' idiom guarantees proper resource cleanup.
			// We request 30 UpdateFrame events per second, and unlimited
			// RenderFrame events (as fast as the computer can handle).
			using (var game = new TestBench2())
			{
				game.Run(30.0);
			}
		}

		protected override void setupScene()
		{
			base.setupScene();

			laserOccDiskScene3d = new SSScene(mainShader, pssmShader, instancingShader, instancingPssmShader);
			laserFlareScene2d = new SSScene(mainShader, pssmShader, instancingShader, instancingPssmShader);

			var mesh = SSAssetManager.GetInstance<SSMesh_wfOBJ>("./drone2/Drone2.obj");

			// add drones
			droneObj1 = new SSObjectMesh(mesh);
			droneObj1.Pos = new OpenTK.Vector3(-20f, 0f, -15f);
			droneObj1.Orient(Quaternion.FromAxisAngle(Vector3.UnitY, (float)Math.PI / 2f));
			//droneObj1.renderState.visible = false;
			droneObj1.Name = "attacker drone";
			main3dScene.AddObject(droneObj1);

			droneObj2 = new SSObjectMesh(mesh);
			droneObj2.Pos = new OpenTK.Vector3(20f, 0f, -15f);
			droneObj2.Name = "target drone";
            droneObj2.MainColor = new Color4(1f, 0f, 0.7f, 1f);
            //droneObj2.renderState.visible = false;
			main3dScene.AddObject (droneObj2);

			// manages laser objects
            laserManager = new SLaserManager(alpha3dScene, laserOccDiskScene3d, laserFlareScene2d);

			// tweak the laser start point (by adding an offset in object-local coordinates)
			laserSourceTxfm = Matrix4.CreateTranslation (0f, 1f, 2.75f);

            // debugging snippets:
            //droneObj1.MainColor = Color4.Green;
            //droneObj1.renderState.visible = false;
    	}

		protected void _createLaser()
		{
			var laserParams = new SLaserParameters ();
			laserParams.numBeams = rand.Next (1, 6);
			if (laserParams.numBeams == 2) {
				// 2's don't look too great
				laserParams.numBeams = 1;
			}
			laserParams.beamStartPlacementScale = 2f * (float)rand.NextDouble ();
			laserParams.beamDestSpread = (float)Math.Pow (rand.NextDouble (), 3.0)
				* laserParams.beamStartPlacementScale;

			laserParams.backgroundColor = Color4Helper.RandomDebugColor ();
			laserParams.overlayColor = Color4.White;
			laserParams.middleInterferenceColor = Color4.White;

            //laserParams.driftModulationFuncStr = (rand.NextDouble() * 0.1).ToString();

			var newLaser = laserManager.addLaser (laserParams, droneObj1, droneObj2);
			newLaser.sourceTxfm = laserSourceTxfm;
            newLaser.beamObstacles = new List<SSObject> ();
            newLaser.beamObstacles.Add(droneObj2);
			
            activeLaser.Target = newLaser;
		}

        protected override void renderOcclusion3dScenes (
            float fovy, float aspect, float nearPlane, float farPlane, 
            ref Matrix4 mainSceneView, ref Matrix4 mainSceneProj, 
            ref Matrix4 rotationOnlyView, ref Matrix4 screenProj)
        {
            base.renderOcclusion3dScenes(fovy, aspect, nearPlane, farPlane, ref mainSceneView, ref mainSceneProj, ref rotationOnlyView, ref screenProj);

            // laser occlusion test disks (not visible)
            laserOccDiskScene3d.renderConfig.invCameraViewMatrix = mainSceneView;
            laserOccDiskScene3d.renderConfig.projectionMatrix = mainSceneProj;
            laserOccDiskScene3d.Render();
        }

        protected override void renderScreen2dScenes (
            float fovy, float aspect, float nearPlane, float farPlane, 
            ref Matrix4 mainSceneView, ref Matrix4 mainSceneProj, 
            ref Matrix4 rotationOnlyView, ref Matrix4 screenProj)
        {
            // render before HUD and sun flare
            laserFlareScene2d.renderConfig.projectionMatrix = screenProj;
            laserFlareScene2d.Render();

            base.renderScreen2dScenes(fovy, aspect, nearPlane, farPlane, ref mainSceneView, ref mainSceneProj, ref rotationOnlyView, ref screenProj);
        }

		protected void laserKeyDownHandler(object sender, KeyboardKeyEventArgs e)
		{
			if (!base.Focused) return;

			if (e.Key == Key.Q) {
				if (activeLaser.Target == null) {
					_createLaser ();
				}
			}
		}

		protected void laserKeyUpHandler(object sender, KeyboardKeyEventArgs e)
		{
            if (e.Key == Key.Q) {
                if (activeLaser.Target != null) {
                    var laser = activeLaser.Target as SLaser;
                    laser.release();
                    activeLaser.Target = null;
                }
            } else if (e.Key == Key.M) {
                var camera = (main3dScene.ActiveCamera as SSCameraThirdPerson);
                if (camera != null) {
                    var target = camera.FollowTarget;
                    if (target == null) {
                        camera.FollowTarget = droneObj1;
                    } else if (target == droneObj1) {
                        camera.FollowTarget = droneObj2;
                    } else {
                        camera.FollowTarget = null;
                    }
                    updateTextDisplay();
                }
            }
		}

		protected override void updateTextDisplay ()
		{
			base.updateTextDisplay ();
            textDisplay.Label += "\n\nPress Q to engage a laser";

            var camera = main3dScene.ActiveCamera as SSCameraThirdPerson;
            if (camera != null) {
                var target = camera.FollowTarget;
                textDisplay.Label += 
                    "\n\nPress M to toggle camera target: ["
                    + (target == null ? "none" : target.Name) + ']';
            }

	    }

		protected override void setupInput ()
		{
			base.setupInput ();
			this.KeyUp += laserKeyUpHandler;
			this.KeyDown += laserKeyDownHandler;
		}

		protected override void setupCamera()
		{
            var camera = new SSCameraThirdPerson (droneObj2);
			//var camera = new SSCameraThirdPerson (droneObj1);
			camera.Pos = Vector3.Zero;
			camera.followDistance = 80.0f;

			main3dScene.ActiveCamera = camera;
		}
	}
}

