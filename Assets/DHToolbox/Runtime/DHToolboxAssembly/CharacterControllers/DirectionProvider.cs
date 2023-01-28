using UnityEngine;

namespace DHToolbox.Runtime.CharacterControllers
{
    public abstract class DirectionProvider : MonoBehaviour
    {
        public abstract Vector3 Direction { get; }
    }
}