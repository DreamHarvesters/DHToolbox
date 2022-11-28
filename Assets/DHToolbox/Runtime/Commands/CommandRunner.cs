using System;
using UnityEngine;

namespace DHToolbox.Runtime.Commands
{
    public class CommandRunner : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICommand[] commands;

        public void Run() => Array.ForEach(commands, command => command.Execute());
    }
}