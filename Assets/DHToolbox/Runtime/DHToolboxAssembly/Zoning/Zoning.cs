#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif
#endif
#if UNITY_EDITOR
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DHToolbox.Runtime.DHToolboxAssembly.Zoning
{
    [CreateAssetMenu(
        fileName = nameof(Zoning),
        menuName = Constants.CreateMenuCategory + "/" + nameof(Zoning),
        order = 0
    )]
    public class Zoning : ScriptableObject
    {
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [TitleGroup("Operations")]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton)]
        [InfoBox(
            "Sets up given number of zones with given materials. All have the same materials. This is a shortcut to create multiple zones which share the same materials."
        )]
#endif
        public void Setup(int zoneCount, Material[] materials)
        {
            zones = new List<Zone>(zoneCount);

            for (int i = 0; i < zoneCount; i++)
            {
                List<ZonedMaterial> zonedMaterials = Enumerable
                    .Range(0, materials.Length)
                    .Select(index => new ZonedMaterial(materials[index]))
                    .ToList();

                zones.Add(new Zone(this, new List<ZonedMaterial>(zonedMaterials)));
            }
        }

#if ODIN_INSPECTOR
        [TitleGroup("Operations")]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton)]
#endif
        public void AddMaterialToAll(Material material)
        {
            foreach (Zone zone in zones)
            {
                zone.AddMaterial(material);
            }
        }

        // [TitleGroup("Operations")]
        // [Button(ButtonSizes.Large), GUIColor(1, 0, 0)]
        public void Clear()
        {
            if (
                EditorUtility.DisplayDialog(
                    "Clear All",
                    "You will lose all the zone settings permanentyl. Are you sure to continue?",
                    "Yes",
                    "No"
                )
            )
                zones = new List<Zone>();
        }

        public void Remove(Zone z)
        {
            zones.Remove(z);
        }

        private void OnEnable()
        {
            zones?.ForEach(zone => zone.Init(this));
        }

        public void AddZone(Zone zone)
        {
            if (!zones.Contains(zone))
                zones.Add(zone);
        }

        private void DrawZonesTitleBar()
        {
#if ODIN_INSPECTOR
#if UNITY_EDITOR
            if (SirenixEditorGUI.ToolbarButton("Clear"))
                Clear();
#endif
#endif
        }
#endif

#if ODIN_INSPECTOR
        [ListDrawerSettings(
            HideRemoveButton = true,
            DraggableItems = false,
            OnTitleBarGUI = "DrawZonesTitleBar",
            ShowIndexLabels = true
        )]
#endif
        [SerializeField]
        private List<Zone> zones;

        private Subject<Zone> zoneApplied = new Subject<Zone>();

        public IObservable<Zone> ObserveZoneApplied
        {
            get { return zoneApplied; }
        }

        private int zoneCount
        {
            get { return zones.Count - 1; }
        }

#if ODIN_INSPECTOR
        [TitleGroup("Test")]
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton)]
        public void Apply([PropertyRange(0, "zoneCount")] int index)
        {
            if (index >= zones.Count)
                throw new InvalidOperationException("Index is bigger than zone count");

            current = index;
            Apply(zones[current]);
        }
#endif

        public void ApplyRandom()
        {
            if (zones.Count > 0)
            {
                current = Random.Range(0, zones.Count);
                Apply(zones[current]);
            }
        }

        private int current = 0;

        public Zone Current
        {
            get { return zones[current]; }
        }

        public void ApplyOrdered()
        {
            if (zones.Count > 0)
            {
                current = ++current % zones.Count;
                Apply(zones[current]);
            }
        }

        private void Apply(Zone z)
        {
            z.Apply();
            zoneApplied.OnNext(z);
        }
    }
}
