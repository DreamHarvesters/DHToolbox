using DHToolbox.Runtime.Persistency;
using UnityEngine;

namespace DHToolbox.Runtime.AppConfig
{
    public class AppConfig : ScriptableObject
    {
        public virtual void Configure()
        {
            ServiceLocator.ServiceLocator.AddService<Game.Game>(new Game.Game());
            ServiceLocator.ServiceLocator.AddService<IPersistency>(new PlayerPrefsPersistency());
        }
    }
}