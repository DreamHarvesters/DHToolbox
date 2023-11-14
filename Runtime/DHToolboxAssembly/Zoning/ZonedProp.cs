using UniRx;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Zoning
{
    public class ZonedProp : MonoBehaviour
    {
        [SerializeField] protected string ownerZoneTag;

        public virtual void Init(Zoning zoning)
        {
            zoning.ObserveZoneApplied.Subscribe(delegate(Zone zone)
            {
                gameObject.SetActive(zone.Tag.Equals(ownerZoneTag));
            }).AddTo(gameObject);
        }
    }
}