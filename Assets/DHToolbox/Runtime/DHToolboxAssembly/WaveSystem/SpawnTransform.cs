using DHToolbox.Runtime.DHToolboxAssembly.Utils.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DHToolbox.Runtime.DHToolboxAssembly.WaveSystem
{
    public class SpawnTransform : MonoBehaviour
    {
        [SerializeField] private float radius;

        public Vector3 RandomPosition => transform.position + Random.insideUnitCircle.ToV3_XZ() * radius;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}