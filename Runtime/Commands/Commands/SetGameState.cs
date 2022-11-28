using System;
using DHToolbox.Runtime.Game;
using GameFoundations.Runtime.ServiceLocator;
using UnityEngine;

namespace DHToolbox.Runtime.Commands.Commands
{
    [Serializable]
    public class SetGameState : ICommand
    {
        [SerializeField] private GameState targetState;

        public void Execute()
        {
            ServiceLocator.Game.SetState(targetState);
        }
    }
}