using System;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.CurveBasedVariable
{
    [Serializable]
    public class CurvedFloatVariable : CurvedVariable<float>
    {
        public override float Evaluate(float normalizedRate) =>
            Mathf.Clamp(curve.Evaluate(normalizedRate) * (max - min) + min, min, max);
    }
}