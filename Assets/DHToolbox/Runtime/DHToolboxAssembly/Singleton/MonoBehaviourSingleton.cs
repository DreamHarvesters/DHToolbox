using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Singleton
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<T>();
                    if (!instance)
                    {
                        var newGameObject = new GameObject($"{typeof(T)}_Singleton");
                        instance = newGameObject.AddComponent<T>();
                    }
                }

                return instance;
            }
        }
    }
}