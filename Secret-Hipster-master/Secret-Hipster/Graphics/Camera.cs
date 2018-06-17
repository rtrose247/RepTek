using OpenTK;
using System;

namespace Secret_Hipster.Graphics
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Orientation { get; private set; }
        public float MoveSpeed { get; private set; }
        public float MouseSensitivity { get; private set; }
        public Matrix4 PerspectiveFieldOfViewMatrix { get; private set; }

        public Camera(int width, int height)
        {
            this.Init(width, height);
        }

        private void Init(int width, int height)
        {
            this.Position = Vector3.Zero;
            this.Orientation = new Vector3((float)Math.PI, 0, 0);
            this.MoveSpeed = 0.2f;
            this.MouseSensitivity = 0.01f;
            this.PerspectiveFieldOfViewMatrix = Matrix4.CreatePerspectiveFieldOfView(1.3f, width / (float)height, 0.5f, 120f);
        }

        public Matrix4 GetViewMatrix()
        {
            var lookat = new Vector3();

            lookat.X = (float)(Math.Sin(Orientation.X) * Math.Cos(Orientation.Y));
            lookat.Y = (float)Math.Sin(Orientation.Y);
            lookat.Z = (float)(Math.Cos(Orientation.X) * Math.Cos(Orientation.Y));

            return Matrix4.LookAt(Position, Position + lookat, Vector3.UnitY);
        }

        public Matrix4 GetViewProjectionMatrix()
        {
            return this.GetViewMatrix() * this.PerspectiveFieldOfViewMatrix;
        }

        public void Move(float x, float y, float z)
        {
            var offset = new Vector3();

            var forward = new Vector3((float)Math.Sin(Orientation.X), (float)Math.Sin(Orientation.Y), (float)Math.Cos(Orientation.X));
            var right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += z * forward;
            offset.Y += y;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, MoveSpeed);

            Position += offset;
        }

        public void AddRotation(float x, float y)
        {
            x = x * MouseSensitivity;
            y = y * MouseSensitivity;

            var orientationX = (Orientation.X + x) % ((float)Math.PI * 2.0f);
            var orientationY = Math.Max(Math.Min(Orientation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);

            Orientation = new Vector3(orientationX, orientationY, this.Orientation.Z);
        }
    }
}
