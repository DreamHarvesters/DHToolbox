using UnityEngine;

namespace GameFoundations.Runtime.CurveBasedVariable
{
    public class CurvedIntVariable : CurvedVariable<int>
    {
        public override float Evaluate(float normalizedRate)
        {
            return Mathf.Clamp(curve.Evaluate(normalizedRate) * max, min, max);
        }
    }
}