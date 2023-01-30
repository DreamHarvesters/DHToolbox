using System;

namespace DHToolbox.Runtime.DHToolboxAssembly.Commands.Commands
{
    public abstract class GameCommand : ICommand
    {
        protected Game.Game game;

        public void Initializa()
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