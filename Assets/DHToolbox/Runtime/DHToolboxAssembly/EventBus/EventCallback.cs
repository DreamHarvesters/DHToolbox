using System;
using System.Collections;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DHToolbox.Runtime.DHToolboxAssembly.EventBus
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
            ServiceLocator.ServiceLocator.GetService<EventBus>().AsObservable(Type.GetType(eventToListen))
                .Subscribe(_ => OnRaised?.Invoke()).AddTo(gameObject);
        }
    }
}