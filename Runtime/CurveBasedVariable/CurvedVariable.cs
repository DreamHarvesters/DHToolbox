using System;
using UnityEngine;

namespace DHToolbox.Runtime.CurveBasedVariable
{
    [Serializable]
    public abstract class CurvedVariable<T>
    {
        [SerializeField] protected T min;
        [SerializeField] protected T max;
        [SerializeField] protected AnimationCurve curve;

        public T Max => max;

        public T Min => min;

        public abstract T Evaluate(float normalizedRate);
    }
}