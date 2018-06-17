using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Secret_Hipster.Graphics
{
    public class Buffer
    {
        private readonly int memoryLocation;
        public int Length { get; private set; }
        public int MemoryLocation 
        {
            get
            {
                return memoryLocation;
            }
        }

        public Buffer(Vector2[] data)
        {
            GL.GenBuffers(1, out memoryLocation);

            GL.BindBuffer(BufferTarget.ArrayBuffer, MemoryLocation);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector2.SizeInBytes), data, BufferUsageHint.StaticDraw);

            this.Length = data.Length;
        }
        public Buffer(Vector3[] data)
        {
            GL.GenBuffers(1, out memoryLocation);

            GL.BindBuffer(BufferTarget.ArrayBuffer, MemoryLocation);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector3.SizeInBytes), data, BufferUsageHint.StaticDraw);

            this.Length = data.Length;
        }
        public Buffer(Vector4[] data)
        {
            GL.GenBuffers(1, out memoryLocation);

            GL.BindBuffer(BufferTarget.ArrayBuffer, MemoryLocation);
            GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector4.SizeInBytes), data, BufferUsageHint.StaticDraw);

            this.Length = data.Length;
        }
    }
}