using UnityEngine;

namespace DHToolbox.Runtime.CurveBasedVariable
{
    [CreateAssetMenu(fileName = nameof(CurvedVariableAsset),
        menuName = Constants.CreateMenuCategory + "/" + nameof(CurvedVariableAsset),
        order = 0)]
    public class CurvedVariableAsset : ScriptableObject
    {
        [SerializeReference] public CurvedVariable Variable;
    }
}