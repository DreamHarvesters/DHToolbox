using DHToolbox.Runtime.Game;
using GameFoundations.Runtime.AppConfig;
using GameFoundations.Runtime.Persistency;
using UnityEngine;

namespace GameFoundations.Runtime.ServiceLocator
{
    public static class ServiceLocator
    {
        private static EventBus.EventBus eventBus = new();

        public static EventBus.EventBus EventBus => eventBus;

        public static Game Game { get; set; }

        public static IPersistency Persistency { get; set; }

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            var config = Resources.Load<AppConfig.AppConfig>(nameof(AppConfig.AppConfig));
            if (!config)
                config = ScriptableObject.CreateInstance<AppConfig.AppConfig>();

            config.Configure();
        }
    }
}