using UnityEngine;

namespace GameFoundations.Runtime.Singleton
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

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