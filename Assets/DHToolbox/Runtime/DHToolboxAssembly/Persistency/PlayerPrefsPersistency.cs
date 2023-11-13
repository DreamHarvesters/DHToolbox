using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Persistency
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

        public virtual void DeleteKey(string key) => PlayerPrefs.DeleteKey(key);

        public virtual void DeleteAll() => PlayerPrefs.DeleteAll();
        
        public virtual int GetInt(string key, int defaultValue) => PlayerPrefs.GetInt(key, defaultValue);
        public virtual float GetFloat(string key, float defaultValue) => PlayerPrefs.GetFloat(key, defaultValue);
        public virtual string GetString(string key, string defaultValue) => PlayerPrefs.GetString(key, defaultValue);

        public virtual bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public virtual void Save() => PlayerPrefs.Save();
    }
}