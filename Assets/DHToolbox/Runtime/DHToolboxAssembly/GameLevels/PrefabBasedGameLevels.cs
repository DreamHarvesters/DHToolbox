using Cysharp.Threading.Tasks;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Game;
using DHToolbox.Runtime.DHToolboxAssembly.Game.Events;
using DHToolbox.Runtime.DHToolboxAssembly.Game.Initialization;
using DHToolbox.Runtime.DHToolboxAssembly.Indexing;
using DHToolbox.Runtime.DHToolboxAssembly.ServiceLocator;
using UniRx;
using UnityEngine;

#if ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif

namespace GameAssets.Scripts
{
    public class PrefabBasedGameLevels : IInitializable
    {
        public IndexProvider IndexProvider { get; set; }

        public int CurrentPrefabIndex => IndexProvider.Current;

        private int currentLoadedReferenceIndex;
        private GameObject loadedReferenceInstance;
#if ADDRESSABLES
        private AssetReference[] prefabReferences;

        public PrefabBasedGameLevels(IndexProvider indexProvider, AssetReference[] references)
        {
            prefabReferences = references;
            IndexProvider = indexProvider;

            ServiceLocator.GetService<EventBus>().AsObservable<BeforeInitializeEvent>()
                .Subscribe(initEvent => initEvent.Initializables.Add(this));
        }
#endif

        public UniTask Initialize()
        {
#if ADDRESSABLES
            var eventBus = ServiceLocator.GetService<EventBus>();

            var gameStateChanged = eventBus.AsObservable<AfterGameStateChanged>();
            gameStateChanged.Where(changed => changed.NewState == GameState.Success)
                .Do(_ => IndexProvider.MoveNext())
                .Subscribe();

            gameStateChanged.Where(changed => changed.NewState == GameState.LoadingLevel)
                .Select(_ =>
                {
                    if (loadedReferenceInstance)
                    {
                        prefabReferences[currentLoadedReferenceIndex].ReleaseInstance(loadedReferenceInstance);
                        GameObject.Destroy(loadedReferenceInstance);
                    }
                    return Observable.ReturnUnit();
                })
                .Switch()
                .Do(_ => currentLoadedReferenceIndex = CurrentPrefabIndex)
                .Select(_ =>
                    prefabReferences[CurrentPrefabIndex].LoadAssetAsync<GameObject>().ToUniTask().ToObservable())
                .Switch()
                .Select(o => GameObject.Instantiate(o))
                .Do(instance => loadedReferenceInstance = instance)
                .Subscribe(_ => ServiceLocator.GetService<Game>().CompleteLevelLoading());
#endif
            return UniTask.CompletedTask;
        }
    }
}