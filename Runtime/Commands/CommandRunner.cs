using System;
using UnityEngine;

namespace DHToolbox.Runtime.Commands
{
    public class CommandRunner : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICommand[] commands;

        private void ForeachCommand(Action<ICommand> foreachDlg) => Array.ForEach(commands, foreachDlg);

        private void Awake() => ForeachCommand(command => command.Initializa());


        public void Run() => ForeachCommand(command => command.Execute());
    }
}