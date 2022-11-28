using DHToolbox.Runtime.Game;
using GameFoundations.Runtime.Persistency;
using UnityEngine;

namespace GameFoundations.Runtime.AppConfig
{
    public class AppConfig : ScriptableObject
    {
        public virtual void Configure()
        {
            ServiceLocator.ServiceLocator.Game = new Game();
            ServiceLocator.ServiceLocator.Persistency = new PlayerPrefsPersistency();
        }
    }
}