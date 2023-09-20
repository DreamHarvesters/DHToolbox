using System;
using System.Collections;
using System.Linq;
using DHToolbox.Runtime.DHToolboxAssembly.CurveBasedVariable;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.ServiceLocator;
using DHToolbox.Runtime.DHToolboxAssembly.UpgradeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameAssets.Scripts
{
    [CreateAssetMenu]
    public class UpgradeSetup : ScriptableObject, IEventSender
    {
        [Button]
        public virtual void Apply(IUpgradableAttributes attributes)
        {
            var type = attributes.GetType();
            var property = type.GetProperty(this.property);
            if (property == null)
                throw new Exception($"Invalid property: {this.property}");

            var currentValue = (int)property.GetValue(attributes);
            property.SetValue(attributes, Mathf.Clamp(currentValue + increasePerUpgrade, 0, MaxLevel));

            ServiceLocator.GetService<EventBus>().Raise(new UpgradedEvent(attributes, this));
        }

#if ODIN_INSPECTOR
        [ValueDropdown(nameof(AttributeClasses))]
#endif
        [SerializeField]
        private string className;

#if ODIN_INSPECTOR
        private static IEnumerable AttributeClasses => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IUpgradableAttributes).IsAssignableFrom(type) && type.IsClass)
            .Select(type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName));
#endif


#if ODIN_INSPECTOR
        [ValueDropdown(nameof(UpgradableAttributes))]
#endif
        [SerializeField]
        private string property;

        private IEnumerable UpgradableAttributes => string.IsNullOrEmpty(className)
            ? Enumerable.Empty<string>()
            : Type.GetType(className).GetProperties().Select(info => info.Name);


        [SerializeField] private string upgradeTextOnUI;

        [SerializeField] private int increasePerUpgrade = 1;

        [Tooltip(
            "This is used as the dividend of increasePerUpgrade value. Property will be modified as Current Value += Increase Per Upgrade / Increase Dividend")]
        [SerializeField]
        private int maxLevel = 100;

        [SerializeField] private CurvedIntVariable costCurve;

        [SerializeField] private CurvedFloatVariable skillEffectCurve;

        public int IncreasePerUpgrade => increasePerUpgrade;

        public int MaxLevel => maxLevel;

        public string Property => property;

        public string UpgradeTextOnUI => upgradeTextOnUI;

        public float CurrentLevel(IUpgradableAttributes attributes)
        {
            var type = attributes.GetType();
            var property = type.GetProperty(this.property);
            if (property == null)
                throw new Exception($"Invalid property: {this.property}");

            return Convert.ToSingle(property.GetValue(attributes)) * maxLevel;
        }

        public int CostOfLevel(int level)
        {
            return costCurve.Evaluate((float)level / maxLevel);
        }

        public int CostOfNextLevel(IUpgradableAttributes attributes) =>
            CostOfLevel((int)(CurrentLevel(attributes) + 1));

        public float CurrentLevelEffect(IUpgradableAttributes attributes) =>
            skillEffectCurve.Evaluate((float)CurrentLevel(attributes) / maxLevel);

        public float Min => skillEffectCurve.Min;

        public float Max => skillEffectCurve.Max;

        public float NormalizedLevelEffect(IUpgradableAttributes attributes) =>
            (CurrentLevelEffect(attributes) - Min) / (Max - Min);
    }
}