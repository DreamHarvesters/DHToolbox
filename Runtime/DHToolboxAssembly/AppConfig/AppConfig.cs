using DHToolbox.Runtime.DHToolboxAssembly.Persistency;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.AppConfig
{
    public class AppConfig : ScriptableObject
    {
        public virtual void ConfigureAfterAssembliesLoaded()
        {
            ServiceLocator.ServiceLocator.AddService<Game.Game>(new Game.Game());
            ServiceLocator.ServiceLocator.AddService<IPersistency>(new PlayerPrefsPersistency());
            ServiceLocator.ServiceLocator.AddService<EventBus.EventBus>(new EventBus.EventBus());
        }
        
        public virtual void ConfigureAfterSceneLoaded(){}
    }
}