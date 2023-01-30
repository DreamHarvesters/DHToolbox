using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.CharacterControllers
{
    public abstract class DirectionProvider : MonoBehaviour
    {
        public abstract Vector3 Direction { get; }
    }
}