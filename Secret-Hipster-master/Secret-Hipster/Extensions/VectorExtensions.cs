using OpenTK;

namespace Secret_Hipster.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 Truncate(this Vector3 vector, float max)
        {
            var i = max / vector.Length;
            i = i < 1.0f ? i : 1.0f;
            return vector * i;
        }
    }
}