using Conscript;
using Conscript.Compiler;
using Conscript.Runtime;
using FastBitmapLib;
using ca.axoninteractive.Geometry.Hex;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using QuickFont.Configuration;
using Sanford.Multimedia.Midi;
using Secret_Hipster;
using Secret_Hipster.Graphics;
using Secret_Hipster.OpenGLPrograms;
using Secret_Hipster.Primitives;
using Secret_Hipster.Util;
using SimpleScene;
using SimpleScene.Demos;
using System;
using System.Drawing;
using System.Collections.Generic;
using Prolog;



namespace RepTek
{
    public class Game : TestBenchBootstrap //GameWindow
    {
        private Camera camera;
        private Spritebatch spritebatch;
        private QuadHandler quadHandler;
        private SierpinskiCarpet sierpinskiCarpet;
        private Grid grid;
        private Vector2 lastMousePos;
        //
        private PrologEngine myPrologBrain;
        //
        private ScriptManager m_scriptManager;
        private ScriptLoader m_scriptLoaderDefault;
        //
        public class MidiHelper
        {
            private bool playing = false;
            private bool closing = false;
            private OutputDevice outDevice;
            private int outDeviceID = 0;
        }
        //
        private static HexGrid hexgrid = new HexGrid(2f);
        private float offset = 0.5f * hexgrid.HexRadius;
        //
        public Game()
            : base("RepTek")
        {
            // prolog engine
            myPrologBrain = new PrologEngine();
            // script manager
            m_scriptManager = new ScriptManager();
        }


        // Settings, load textures, sounds
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //VSync = VSyncMode.On;
            //GL.Viewport(0, 0, Width, Height);

            //lastMousePos = new Vector2();
            //camera = new Camera(Width, Height);

            //camera.Position = new Vector3(0, 10, 20);

            //spritebatch = new Spritebatch(camera);
            //grid = new Grid(Color.White);
            //quadHandler = new QuadHandler();
            //sierpinskiCarpet = new SierpinskiCarpet(12);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.ClearColor(Color.CornflowerBlue);
        }

        //SS function(s)
        //
        protected override void setupHUD()
        {
            base.setupHUD();

            // HUD text....
            //var testDisplay = new SSObject2DSurface_AGGText();
            //testDisplay.backgroundColor = Color.Transparent;
            //testDisplay.alphaBlendingEnabled = true;
            //testDisplay.Label = "TEST AGG";
            //hud2dScene.AddObject(testDisplay);
            //testDisplay.Pos = new Vector3(50f, 100f, 0f);
            //testDisplay.Scale = new Vector3(1.0f);
        }

        protected override void setupScene()
        {
            base.setupScene();

            var mesh = SSAssetManager.GetInstance<SSMesh_wfOBJ>("./blade_runner_police_spinner_blue.rot.4.obj");

            // add drone
            SSObject droneObj = new SSObjectMesh(mesh);
            main3dScene.AddObject(droneObj);
            droneObj.renderState.lighted = true;
            //droneObj.EulerDegAngleOrient(-40.0f,0.0f);
            droneObj.Pos = new OpenTK.Vector3(0f, 0f, -15f);
            droneObj.Name = "drone 1";

            // add second drone

            SSObject drone2Obj = new SSObjectMesh(
                SSAssetManager.GetInstance<SSMesh_wfOBJ>("./blade_runner_police_spinner_blue.rot.4.obj")
            );
            main3dScene.AddObject(drone2Obj);
            drone2Obj.renderState.lighted = true;
            drone2Obj.EulerDegAngleOrient(-40f, 0f);
            drone2Obj.Pos = new OpenTK.Vector3(-10f, -10f, 0f);
            drone2Obj.Name = "drone 2";
        }

        //
            /// <summary>
            /// Called when it is time to setup the next frame. Add you game logic here.
            /// </summary>
            /// <param name="e">Contains timing information for framerate independent logic.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

        }

    }
}
