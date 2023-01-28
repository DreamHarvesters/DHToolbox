using System;
using System.Linq;
using Foundations.Scripts.Utils.Extensions;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameAssets.Scripts.Waves
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