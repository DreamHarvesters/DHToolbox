using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.ObjectFactory
{
    public class InstantiaterFactory : IObjectFactory
    {
        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(prefab, position, rotation);
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return GameObject.Instantiate(prefab, position, rotation, parent);
        }

        public GameObject Instantiate(GameObject prefab, Transform parent, bool worldPositionStays)
        {
            return GameObject.Instantiate(prefab, parent, worldPositionStays);
        }

        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
            return GameObject.Instantiate(prefab, parent);
        }

        public GameObject Instantiate(GameObject prefab)
        {
            return GameObject.Instantiate(prefab);
        }

        public void Destroy(GameObject gameObject, float delay)
        {
            GameObject.Destroy(gameObject, delay);
        }
    }
}