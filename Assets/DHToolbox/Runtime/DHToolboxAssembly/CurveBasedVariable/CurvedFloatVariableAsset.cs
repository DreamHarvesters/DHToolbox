using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.CurveBasedVariable
{
    [CreateAssetMenu(fileName = nameof(CurvedFloatVariableAsset),
        menuName = Constants.CreateMenuCategory + "/" + nameof(CurvedFloatVariableAsset),
        order = 0)]
    public class CurvedFloatVariableAsset : ScriptableObject
    {
        public CurvedFloatVariable Variable;
    }
}