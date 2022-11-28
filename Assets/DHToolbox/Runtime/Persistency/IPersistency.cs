namespace GameFoundations.Runtime.Persistency
{
    public interface IPersistency
    {
        void SetInt(string key, int value);

        void SetFloat(string key, float value);

        void SetString(string key, string value);
    }
}