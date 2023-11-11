using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    public interface IObjectFactory
    {
        GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
        GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent);
        GameObject Instantiate(GameObject prefab, Transform parent, bool worldPositionStays);
        GameObject Instantiate(GameObject prefab, Transform parent);
        GameObject Instantiate(GameObject prefab);
        void Destroy(GameObject gameObject, float delay);
    }
}