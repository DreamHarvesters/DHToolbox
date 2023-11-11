namespace DHToolbox.Runtime.DHToolboxAssembly.Persistency
{
    public interface IPersistency
    {
        void SetInt(string key, int value);

        void SetFloat(string key, float value);

        void SetString(string key, string value);

        void DeleteKey(string key);

        void DeleteAll();

        int GetInt(string key, int defaultValue);
        float GetFloat(string key, float defaultValue);
        string GetString(string key, string defaultValue);

        bool HasKey(string key);

        void Save();
    }
}