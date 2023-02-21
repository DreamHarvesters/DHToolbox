using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.CurveBasedVariable
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

#if ODIN_INSPECTOR
        [Button]
        void LogValue(float rate) => Debug.Log($"Value: {Evaluate(rate)}");

        [Button]
        void LogInterval(float from, float to, float step)
        {
            for (float i = from; i < to; i += step)
            {
                LogValue(i);
            }
        }
#endif
    }
}