using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Utils.Extensions
{
    public static class ColliderExtensions
    {
        public static Vector3 RandomPoint(this BoxCollider collider)
        {
            var bounds = collider.bounds;
            return new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
        }
    }
}