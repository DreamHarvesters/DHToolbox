using System;

namespace DHToolbox.Runtime.Game
{
    public class InvalidGameStateTransitionException : Exception
    {
        public InvalidGameStateTransitionException() : base("Invalid game state transition")
        {
        }
    }
}