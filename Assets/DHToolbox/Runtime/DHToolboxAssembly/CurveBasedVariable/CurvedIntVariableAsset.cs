using UnityEngine;

namespace DHToolbox.Runtime.CurveBasedVariable
{
    [CreateAssetMenu(fileName = nameof(CurvedIntVariableAsset),
        menuName = Constants.CreateMenuCategory + "/" + nameof(CurvedIntVariableAsset),
        order = 0)]
    public class CurvedIntVariableAsset : ScriptableObject
    {
        public CurvedIntVariable Variable;
    }
}