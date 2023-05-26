using UniRx;
using UnityEngine;

namespace TemplateAssets.Scripts.Zoning
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