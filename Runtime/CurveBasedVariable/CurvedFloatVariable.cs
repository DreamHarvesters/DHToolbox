using UnityEngine;

namespace GameFoundations.Runtime.CurveBasedVariable
{
    public class CurvedFloatVariable : CurvedVariable<float>
    {
        public override float Evaluate(float normalizedRate) =>
            Mathf.Clamp(curve.Evaluate(normalizedRate) * max, min, max);
    }
}