using System;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;
using DHToolbox.Runtime.DHToolboxAssembly.ServiceLocator;
using DHToolbox.Runtime.DHToolboxAssembly.Treasure;
using UniRx;
using UnityEngine;

namespace DHToolbox.Runtime.PlayerStorageView
{
    public class PlayerStorageView : MonoBehaviour
    {
        [SerializeField] private DynamicPlayerStorageItem dynamicPlayerStorageItemPrefab;
        [SerializeField] private RectTransform container;

        private void Start()
        {
            var eventBus = ServiceLocator.GetService<EventBus>();

            eventBus.AsObservable<InventoryItemAddedEvent>().Subscribe(addedEvent =>
            {
                var setup = ResourceSetupRepository.Instance.GetById(addedEvent.AddedResource.Id);
                CreateItem(setup, addedEvent.AddedResource.ObserveAmount);
            }).AddTo(gameObject);

            var treasure = ServiceLocator.GetService<Inventory>();
            treasure.ForeachItem(resource =>
            {
                var setup = ResourceSetupRepository.Instance.GetById(resource.Id);
                CreateItem(setup, resource.ObserveAmount);
            });
        }

        void CreateItem(ResourceSetup setup, IObservable<int> reactiveAmount)
        {
            var newItem = Instantiate(dynamicPlayerStorageItemPrefab, container);
            newItem.Setup(setup, reactiveAmount);
        }
    }
}