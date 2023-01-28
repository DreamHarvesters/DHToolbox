using UnityEngine;

namespace DHToolbox.Runtime.CharacterControllers
{
    public abstract class SpeedProvider : MonoBehaviour
    {
        public abstract float Speed { get; }
    }
}