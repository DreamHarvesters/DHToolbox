using System;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Commands.Commands
{
    public class Log : ICommand
    {
        public LogType LogType;
        public string Text;

        public void Initializa()
        {
        }

        public void Execute()
        {
            Action debug = LogType switch
            {
                LogType.Log => () => Debug.Log(Text),
                LogType.Warning => () => Debug.LogWarning(Text),
                LogType.Error => () => Debug.LogError(Text),
            };

            debug();
        }
    }
}