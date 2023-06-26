using System;
using DHToolbox.Runtime.DHToolboxAssembly.Game;
using DHToolbox.Runtime.DHToolboxAssembly.Game.Events;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace DHToolbox.Runtime.DHToolboxAssembly.EventBus
{
    public class GameStateEventCallback : MonoBehaviour
    {
        public UnityEvent Happened;
        public GameState GameState;

        private void Start()
        {
            ServiceLocator.ServiceLocator.GetService<EventBus>().AsObservable<AfterGameStateChanged>()
                .Where(changed => changed.NewState == GameState)
                .Subscribe(_ => Happened?.Invoke())
                .AddTo(gameObject);
        }
    }
}