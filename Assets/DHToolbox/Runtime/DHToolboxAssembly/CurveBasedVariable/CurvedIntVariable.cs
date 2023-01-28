using System;
using UnityEngine;

namespace DHToolbox.Runtime.CurveBasedVariable
{
    [Serializable]
    public class CurvedIntVariable : CurvedVariable<int>
    {
        public override int Evaluate(float normalizedRate)
        {
            return (int)Mathf.Clamp(curve.Evaluate(normalizedRate) * (max - min) + min, min, max);
        }
    }
}