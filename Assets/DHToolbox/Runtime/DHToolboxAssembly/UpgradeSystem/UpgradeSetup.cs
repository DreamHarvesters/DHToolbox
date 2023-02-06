using System;
using System.Collections;
using System.Linq;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DHToolbox.Runtime.DHToolboxAssembly.UpgradeSystem
{
    [CreateAssetMenu]
    public class UpgradeSetup : ScriptableObject, IEventSender
    {
        public virtual void Apply(IUpgradableAttributes attributes)
        {
            var type = attributes.GetType();
            var property = type.GetProperty(this.property);
            if (property == null)
                throw new Exception($"Invalid property: {this.property}");

            var currentValue = (float)property.GetValue(attributes);
            property.SetValue(attributes, currentValue + (float)increasePerUpgrade / 100);

            ServiceLocator.ServiceLocator.GetService<EventBus.EventBus>().Raise(new UpgradedEvent(attributes, this));
        }

#if ODIN_INSPECTOR
        [ValueDropdown(nameof(AttributeClasses))]
#endif
        [SerializeField] private string className;

#if ODIN_INSPECTOR
        private static IEnumerable AttributeClasses => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IUpgradableAttributes).IsAssignableFrom(type) && type.IsClass)
            .Select(type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName));
#endif


#if ODIN_INSPECTOR
        [ValueDropdown(nameof(UpgradableAttributes))]
#endif
        [SerializeField] private string property;

        private IEnumerable UpgradableAttributes => Type.GetType(className).GetProperties().Select(info => info.Name);


        [SerializeField] private string upgradeTextOnUI;

        [SerializeField] private int increasePerUpgrade = 1;

        [Tooltip(
            "This is used as the dividend of increasePerUpgrade value. Property will be modified as Current Value += Increase Per Upgrade / Increase Dividend")]
        [SerializeField]
        private int increaseDividend = 100;

        [SerializeField] private int cost;

        public int Cost => cost;

        public string Property => property;

        public string UpgradeTextOnUI => upgradeTextOnUI;

        public float CurrentLevel(IUpgradableAttributes attributes)
        {
            var type = attributes.GetType();
            var property = type.GetProperty(this.property);
            if (property == null)
                throw new Exception($"Invalid property: {this.property}");

            return (float)property.GetValue(attributes) * increaseDividend;
        }
    }
}