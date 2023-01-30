using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Utils.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 xy(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }

        public static Vector3 ToV3_XZ(this Vector2 v) => new Vector3(v.x, 0, v.y);

        public static Vector3 WithZ(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
    }
}