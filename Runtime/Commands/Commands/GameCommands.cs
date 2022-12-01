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

    public class StartGame : GameCommand
    {
        public override void Execute()
        {
            game.StartGame();
        }
    }

    public class WinGame : GameCommand
    {
        public override void Execute()
        {
            game.WinGame();
        }
    }

    public class LoseGame : GameCommand
    {
        public override void Execute()
        {
            game.LoseGame();
        }
    }
}