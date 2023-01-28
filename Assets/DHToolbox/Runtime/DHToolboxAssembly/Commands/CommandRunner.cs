using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace DHToolbox.Runtime.Commands
{
    public class CommandRunner : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICommand[] commands;

        private void ForeachCommand(Action<ICommand> foreachDlg) => Array.ForEach(commands, foreachDlg);

        private void Awake() => ForeachCommand(command => command.Initializa());

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Run() => ForeachCommand(command => command.Execute());
    }
}