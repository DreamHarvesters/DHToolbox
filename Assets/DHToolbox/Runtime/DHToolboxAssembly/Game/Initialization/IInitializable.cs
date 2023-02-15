using Cysharp.Threading.Tasks;

namespace DHToolbox.Runtime.DHToolboxAssembly.Game.Initialization
{
    public interface IInitializable
    {
        UniTask Initialize();
    }
}