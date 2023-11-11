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

        public void GetInt(string key, int defaultValue)
        {
#if EASY_SAVE
            ES3.Load(key, defaultValue);
#endif
        }

        public void GetFloat(string key, float defaultValue)
        {
#if EASY_SAVE
            ES3.Load(key, defaultValue);
#endif
        }

        public void GetString(string key, string defaultValue)
        {
#if EASY_SAVE
            ES3.Load(key, defaultValue);
#endif
        }

        public bool HasKey(string key)
        {
#if EASY_SAVE
            return ES3.KeyExists(key);
#endif
        }

        public void Save()
        {
        }
    }
}