using System;
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Foundations.Scripts.UpgradeSystem
{
    [CreateAssetMenu]
    public class UpgradeSetup : ScriptableObject
    {
        public virtual void Apply(IUpgradableAttributes attributes)
        {
            var type = attributes.GetType();
            var property = type.GetProperty(this.property);
            if (property == null)
                throw new Exception($"Invalid property: {this.property}");

            var currentValue = (float)property.GetValue(attributes);
            property.SetValue(attributes, currentValue + (float)increasePerUpgrade / 100);
        }

        [ValueDropdown(nameof(AttributeClasses))] [SerializeField]
        private string className;

        private static IEnumerable AttributeClasses => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IUpgradableAttributes).IsAssignableFrom(type) && type.IsClass)
            .Select(type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName));


        [ValueDropdown(nameof(UpgradableAttributes))] [SerializeField]
        private string property;

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