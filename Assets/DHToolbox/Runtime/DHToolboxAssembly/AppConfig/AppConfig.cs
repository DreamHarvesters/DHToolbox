using Cysharp.Threading.Tasks;
using DHToolbox.Runtime.DHToolboxAssembly.Persistency;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.AppConfig
{
    public class AppConfig : ScriptableObject
    {
        public void ConfigureAfterAssembliesLoaded()
        {
            ServiceLocator.ServiceLocator.AddService<Game.Game>(new Game.Game());
            ServiceLocator.ServiceLocator.AddService<IPersistency>(new PlayerPrefsPersistency());
            ServiceLocator.ServiceLocator.AddService<EventBus.EventBus>(new EventBus.EventBus());

            CustomConfigureAfterAssembliesLoaded();
        }

        public void ConfigureAfterSceneLoaded()
        {
            CustomConfigureAfterSceneLoaded()
                .ContinueWith(() => ServiceLocator.ServiceLocator.GetService<Game.Game>().Initialize());
        }

        public virtual UniTask CustomConfigureAfterAssembliesLoaded() => UniTask.CompletedTask;

        public virtual UniTask CustomConfigureAfterSceneLoaded() => UniTask.CompletedTask;
    }
}