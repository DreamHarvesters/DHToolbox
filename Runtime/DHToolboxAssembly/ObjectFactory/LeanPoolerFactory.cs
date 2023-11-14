using System;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.ObjectFactory
{
    public class LeanPoolerFactory : IObjectFactory
    {
        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
#if LEAN_POOL
            return LeanPool.Spawn(prefab, position, rotation);
#endif
            throw new Exception("Install LeanPool and add LEAN_POOL precompiler definition");
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
#if LEAN_POOL
            return LeanPool.Spawn(prefab, position, rotation, parent);
#endif
            throw new Exception("Install LeanPool and add LEAN_POOL precompiler definition");
        }

        public GameObject Instantiate(GameObject prefab, Transform parent, bool worldPositionStays)
        {
#if LEAN_POOL
            return LeanPool.Spawn(prefab, parent, worldPositionStays);
#endif
            throw new Exception("Install LeanPool and add LEAN_POOL precompiler definition");
        }

        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
#if LEAN_POOL
            return LeanPool.Spawn(prefab, parent);
#endif
            throw new Exception("Install LeanPool and add LEAN_POOL precompiler definition");
        }

        public GameObject Instantiate(GameObject prefab)
        {
#if LEAN_POOL
            return LeanPool.Spawn(prefab);
#endif
            throw new Exception("Install LeanPool and add LEAN_POOL precompiler definition");
        }

        public void Destroy(GameObject gameObject, float delay)
        {
#if LEAN_POOL
            LeanPool.Despawn(gameObject, delay);
#endif
            throw new Exception("Install LeanPool and add LEAN_POOL precompiler definition");
        }
    }
}