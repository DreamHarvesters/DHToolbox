using System;
using System.Collections.Generic;
using Foundations.Scripts.Resource;
using UnityEngine;

namespace Foundations.Scripts.Identification
{
    [CreateAssetMenu(fileName = nameof(Id), menuName = "Toolbox/" + nameof(Id), order = 0)]
    public class Id : ScriptableObject
    {
        public static Id From(string value)
        {
            var key = CreateInstance<Id>();
            key.value = value;
            return key;
        }

        [SerializeField] private string value;

        public string Value => value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is Id key) return key.value.Equals(value);
            if (obj is IResource res) return res.Id.Equals(this);
            if (obj is string keyValue) return keyValue.Equals(this.Value);

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), value);
        }

        private sealed class ValueEqualityComparer : IEqualityComparer<Id>
        {
            public bool Equals(Id x, Id y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.value == y.value;
            }

            public int GetHashCode(Id obj)
            {
                return (obj.value != null ? obj.value.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<Id> ValueComparer { get; } = new ValueEqualityComparer();
    }
}