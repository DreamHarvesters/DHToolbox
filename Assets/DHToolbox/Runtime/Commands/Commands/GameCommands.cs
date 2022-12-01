using System;

namespace DHToolbox.Runtime.Commands.Commands
{
    public abstract class GameCommand : ICommand
    {
        protected Game.Game game;

        protected GameCommand()
        {
            game = ServiceLocator.ServiceLocator.GetService<Game.Game>();
        }

        public abstract void Execute();
    }

    [Serializable]
    public class StartGame : GameCommand
    {
        public override void Execute()
        {
            game.StartGame();
        }
    }

    [Serializable]
    public class WinGame : GameCommand
    {
        public override void Execute()
        {
            game.WinGame();
        }
    }

    [Serializable]
    public class LoseGame : GameCommand
    {
        public override void Execute()
        {
            game.LoseGame();
        }
    }
}