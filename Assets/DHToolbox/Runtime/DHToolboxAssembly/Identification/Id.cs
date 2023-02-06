using System;
using System.Collections.Generic;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Identification
{
    [CreateAssetMenu(fileName = nameof(Id), menuName = Constants.CreateMenuCategory + "/" + nameof(Id), order = 0)]
    public class Id : ScriptableObject
    {
        public static Id From(string value)
        {
            var key = CreateInstance<Id>();
            key.value = value;
            return key;
        }

        [SerializeField] private string value;

        public string Value
        {
            get
            {
                if (!string.IsNullOrEmpty(value))
                    return value;

                return name;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is Id key) return key.Value.Equals(Value);
            if (obj is IResource res) return res.Id.Equals(this);
            if (obj is string keyValue) return keyValue.Equals(this.Value);

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
        }

        private sealed class ValueEqualityComparer : IEqualityComparer<Id>
        {
            public bool Equals(Id x, Id y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Value == y.Value;
            }

            public int GetHashCode(Id obj)
            {
                return (obj.Value != null ? obj.Value.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<Id> ValueComparer { get; } = new ValueEqualityComparer();
    }
}