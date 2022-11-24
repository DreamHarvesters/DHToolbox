using UnityEngine;

namespace GameFoundations.Runtime.CurveBasedVariable
{
    [CreateAssetMenu(fileName = nameof(CurvedVariableAsset),
        menuName = Constants.CreateMenuCategory + "/" + nameof(CurvedVariableAsset),
        order = 0)]
    public class CurvedVariableAsset : ScriptableObject
    {
        [SerializeReference] public CurvedVariable Variable;
    }
}