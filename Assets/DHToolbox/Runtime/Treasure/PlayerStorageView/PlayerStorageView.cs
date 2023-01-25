using System;
using DHToolbox.Runtime.EventBus;
using DHToolbox.Runtime.ServiceLocator;
using Foundations.Scripts.Resource;
using UniRx;
using UnityEngine;

namespace GameAssets.Scripts.UI.PlayerStorageView
{
    public class PlayerStorageView : MonoBehaviour
    {
        [SerializeField] private DynamicPlayerStorageItem dynamicPlayerStorageItemPrefab;
        [SerializeField] private RectTransform container;

        private void Start()
        {
            var eventBus = ServiceLocator.GetService<EventBus>();

            eventBus.AsObservable<ResourceAddedToTreasureEvent>().Subscribe(addedEvent =>
            {
                var setup = ResourceSetupRepository.Instance.GetById(addedEvent.AddedResource.Id);
                CreateItem(setup, addedEvent.AddedResource.ObserveAmount);
            }).AddTo(gameObject);

            var treasure = ServiceLocator.GetService<PlayerTreasure>();
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