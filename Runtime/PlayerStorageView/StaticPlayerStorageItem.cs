using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;
using DHToolbox.Runtime.DHToolboxAssembly.ServiceLocator;
using DHToolbox.Runtime.DHToolboxAssembly.Treasure;

namespace DHToolbox.Runtime.PlayerStorageView
{
    public class StaticPlayerStorageItem : DynamicPlayerStorageItem
    {
        public Id ResourceId;

        private void Awake()
        {
            var playerTreasure = ServiceLocator.GetService<PlayerTreasure>();

            var setup = ResourceSetupRepository.Instance.GetById(ResourceId);
            Setup(setup, playerTreasure.GetOrAdd(ResourceId).ObserveAmount);
        }
    }
}