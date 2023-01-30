using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DHToolbox.Runtime.DHToolboxAssembly.Commands
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