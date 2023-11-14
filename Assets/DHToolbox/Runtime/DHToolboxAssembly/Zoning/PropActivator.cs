using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Zoning
{
    public class PropActivator : MonoBehaviour
    {
        [SerializeField] protected Zoning zoning;

        protected ZonedProp[] props;

        protected virtual void Awake()
        {
            props = GetComponentsInChildren<ZonedProp>(true);
            foreach (var zonedProp in props)
            {
                zonedProp.Init(zoning);
            }
        }
    }
}