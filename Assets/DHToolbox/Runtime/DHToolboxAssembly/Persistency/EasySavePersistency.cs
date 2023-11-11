using System;

namespace DHToolbox.Runtime.DHToolboxAssembly.Persistency
{
    public class EasySavePersistency : IPersistency
    {
        public void SetInt(string key, int value)
        {
#if EASY_SAVE
            ES3.Save(key, value);
#endif
        }

        public void SetFloat(string key, float value)
        {
#if EASY_SAVE
            ES3.Save(key, value);
#endif
        }

        public void SetString(string key, string value)
        {
#if EASY_SAVE
            ES3.Save(key, value);
#endif
        }

        public void DeleteKey(string key)
        {
#if EASY_SAVE
            ES3.DeleteKey(key);
#endif
        }

        public void DeleteAll()
        {
#if EASY_SAVE
            var keys = ES3.GetKeys();
            for (int i = 0; i < keys.Length; i++)
            {
                ES3.DeleteKey(keys[i]);
            }
#endif
        }

        public int GetInt(string key, int defaultValue)
        {
#if EASY_SAVE
            return ES3.Load(key, defaultValue);
#endif
            throw new Exception("Install Easy Save and add EASY_SAVE precompiler definition");
        }

        public float GetFloat(string key, float defaultValue)
        {
#if EASY_SAVE
            return ES3.Load(key, defaultValue);
#endif

            throw new Exception("Install Easy Save and add EASY_SAVE precompiler definition");
        }

        public string GetString(string key, string defaultValue)
        {
#if EASY_SAVE
            return ES3.LoadString(key, defaultValue);
#endif
            throw new Exception("Install Easy Save and add EASY_SAVE precompiler definition");
        }

        public bool HasKey(string key)
        {
#if EASY_SAVE
            return ES3.KeyExists(key);
#endif

            throw new Exception("Install Easy Save and add EASY_SAVE precompiler definition");
        }

        public void Save()
        {
        }
    }
}