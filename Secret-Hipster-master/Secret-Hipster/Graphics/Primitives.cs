using OpenTK;

namespace Secret_Hipster.Graphics
{
    public static class Primitives
    {
        public static Vector3[] CubeVertices = new[]
        {
            new Vector3(-1f, 1f, 0f), // Top Left
            new Vector3(1f, 1f, 0f), // Top Right
            new Vector3(1f, -1f, 0f), // Bottom right
            new Vector3(-1f, -1f, 0f), // bottom left

            new Vector3(-1f, 1f, -2f), // Top Left
            new Vector3(1f, 1f, -2f), // Top Right
            new Vector3(1f, -1f, -2f), // Bottom right
            new Vector3(-1f, -1f, -2f), // bottom left

            new Vector3(-1f, 1f, 0f), 
            new Vector3(-1f, -1f, 0f), 
            new Vector3(-1f, -1f, -2f),
            new Vector3(-1f, 1f, -2f), 

            new Vector3(1f, 1f, 0f), 
            new Vector3(1f, -1f, 0f),
            new Vector3(1f, -1f, -2f), 
            new Vector3(1f, 1f, -2f),

            // Top
            new Vector3(-1f, 1f, 0f), 
            new Vector3(1f, 1f, 0f),
            new Vector3(1f, 1f, -2f), 
            new Vector3(-1f, 1f, -2f),

            // Bottom
            new Vector3(-1f, -1f, 0f),
            new Vector3(1f, -1f, 0f),
            new Vector3(1f, -1f, -2f), 
            new Vector3(-1f, -1f, -2f)
        };

        public static Vector2[] CubeTexturePoints = new[] 
        { 
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),

            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),

            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            // Top
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            // Bottom
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f)
        };
    }
}
