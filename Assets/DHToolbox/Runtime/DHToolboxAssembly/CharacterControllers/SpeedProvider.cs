using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.CharacterControllers
{
    public abstract class SpeedProvider : MonoBehaviour
    {
        public abstract float Speed { get; }
    }
}