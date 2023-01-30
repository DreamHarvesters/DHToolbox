using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace DHToolbox.Runtime.Utils
{
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

    [Serializable]
    public class TagFilter
    {
#if UNITY_EDITOR
        private static IEnumerable TagList() => InternalEditorUtility.tags;
#endif
        public bool EnableFilter;

#if ODIN_INSPECTOR
        [ValueDropdown("TagList")]
#endif
        [SerializeField]
        private List<string> targetTags = new();

        public IReadOnlyList<string> TargetTags => targetTags;

        public bool Contains(string tag) => targetTags.Contains(tag) || !EnableFilter;

        public void Add(string tag) => targetTags.Add(tag);
        public void Remove(string tag) => targetTags.Remove(tag);
    }
}