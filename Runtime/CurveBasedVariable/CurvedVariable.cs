using System;
using UnityEngine;

namespace GameFoundations.Runtime.CurveBasedVariable
{
    [Serializable]
    public abstract class CurvedVariable
    {
    }

    [Serializable]
    public abstract class CurvedVariable<T> : CurvedVariable
    {
        [SerializeField] protected T min;
        [SerializeField] protected T max;
        [SerializeField] protected AnimationCurve curve;

        public T Max => max;

        public T Min => min;

        public abstract float Evaluate(float normalizedRate);
    }
}