using Cysharp.Threading.Tasks;
using DHToolbox.Runtime.DHToolboxAssembly.Persistency;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DHToolbox.Runtime.DHToolboxAssembly.AppConfig
{
    public class AppConfig : ScriptableObject
    {
        public string FirstSceneName;

        public void ConfigureAfterAssembliesLoaded()
        {
            ServiceLocator.ServiceLocator.AddService<Game.Game>(CreateGame());
            ServiceLocator.ServiceLocator.AddService<IPersistency>(CreatePersistency());
            ServiceLocator.ServiceLocator.AddService<EventBus.EventBus>(CreateEventBus());

            CustomConfigureAfterAssembliesLoaded();
        }

        public void ConfigureAfterSceneLoaded()
        {
            CustomConfigureAfterSceneLoaded()
                .ContinueWith(() => UniTask.WaitUntil(() => SceneManager.GetActiveScene().name.Equals(FirstSceneName)))
                .ContinueWith(() => ServiceLocator.ServiceLocator.GetService<Game.Game>().Initialize());
        }

        protected virtual Game.Game CreateGame() => new Game.Game();

        protected virtual IPersistency CreatePersistency() => new PlayerPrefsPersistency();

        protected virtual EventBus.EventBus CreateEventBus() => new EventBus.EventBus();

        protected virtual UniTask CustomConfigureAfterAssembliesLoaded() => UniTask.CompletedTask;

        protected virtual UniTask CustomConfigureAfterSceneLoaded() => UniTask.CompletedTask;
    }
}