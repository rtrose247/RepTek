using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Secret_Hipster;
using Secret_Hipster.Graphics;
using Secret_Hipster.OpenGLPrograms;
using Secret_Hipster.Primitives;
using Secret_Hipster.Util;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepTek
{
    public class Game : GameWindow
    {
        private Camera camera;
        private Spritebatch spritebatch;
        private QuadHandler quadHandler;
        private SierpinskiCarpet sierpinskiCarpet;
        private Grid grid;
        private Vector2 lastMousePos;

        public Game(int width, int height) : base(width, height)
        {
        }

        // Settings, load textures, sounds
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.On;
            GL.Viewport(0, 0, Width, Height);

            lastMousePos = new Vector2();
            camera = new Camera(Width, Height);

            camera.Position = new Vector3(0, 10, 20);

            spritebatch = new Spritebatch(camera);
            grid = new Grid(Color.White);
            quadHandler = new QuadHandler();
            sierpinskiCarpet = new SierpinskiCarpet(12);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.ClearColor(Color.CornflowerBlue);
        }

        // Game logic, input handling
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Focused)
            {
                Vector2 delta = lastMousePos - new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);

                this.camera.AddRotation(delta.X, delta.Y);
                ResetCursor();
            }

            quadHandler.Update(e.Time);

            if (Keyboard[Key.Escape])
            {
                Exit();
            }
        }

        // Render graphics
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            spritebatch.Begin<TextureProgram>();
            quadHandler.Draw(spritebatch);
            spritebatch.End();

            sierpinskiCarpet.Draw(spritebatch);

            grid.Draw(spritebatch);

            SwapBuffers();
        }

        private void ResetCursor()
        {
            OpenTK.Input.Mouse.SetPosition(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
            lastMousePos = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.W:
                    camera.Move(0f, 0f, 0.1f);
                    break;
                case Key.A:
                    camera.Move(-0.1f, 0f, 0f);
                    break;
                case Key.S:
                    camera.Move(0f, 0f, -0.1f);
                    break;
                case Key.D:
                    camera.Move(0.1f, 0f, 0f);
                    break;
                case Key.Up:
                    camera.Move(0f, 0.1f, 0f);
                    break;
                case Key.Down:
                    camera.Move(0f, -0.1f, 0f);
                    break;
            }
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (Focused)
            {
                ResetCursor();
            }
        }
    }
}
