using System;
using System.Collections;
using System.Linq;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.ServiceLocator;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace GameAssets.Scripts.Utils
{
    public class EventCallback : MonoBehaviour
    {
#if ODIN_INSPECTOR
        private static IEnumerable EventTypes => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IEvent).IsAssignableFrom(type))
            .Select(type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName));

        [ValueDropdown(nameof(EventTypes))]
#endif
        [SerializeField]
        private string eventToListen;

        public UnityEvent OnRaised;

        private void Awake()
        {
            ServiceLocator.GetService<EventBus>().AsObservable(Type.GetType(eventToListen))
                .Subscribe(_ => OnRaised?.Invoke()).AddTo(gameObject);
        }
    }
}