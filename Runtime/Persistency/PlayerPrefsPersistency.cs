using UnityEngine;

namespace GameFoundations.Runtime.Persistency
{
    public class PlayerPrefsPersistency : IPersistency
    {
        public virtual void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public virtual void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public virtual void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
    }
}