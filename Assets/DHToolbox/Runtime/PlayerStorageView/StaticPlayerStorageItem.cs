using DHToolbox.Runtime.ServiceLocator;
using Foundations.Scripts.Identification;
using Foundations.Scripts.Resource;

namespace GameAssets.Scripts.UI.PlayerStorageView
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