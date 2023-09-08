using Camera;
using FallObject;
using PlayerSpace;
using Sounds;
using TickableManager;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        ApplicationStartupBindings();
        UIBindings();
        
        Container.Bind<SoundController>().AsSingle().NonLazy();
        Container.Bind<ScoreCounter>().AsSingle().NonLazy();
        Container.Bind<InputController>().AsSingle().NonLazy();

        PlayerBinding();
        
        Container.Bind<PlayerController>().AsSingle().NonLazy();

        Container.Bind<FallObjectSpawner>().AsSingle().NonLazy();
        
        Container.Bind<GameController>()
            .AsSingle()
            .NonLazy();
        
    }

    private void ApplicationStartupBindings()
    {
        Container.BindInterfacesAndSelfTo<UnityEngine.Camera>()
            .FromComponentInNewPrefabResource(ResourcesConst.MainCamera)
            .AsSingle()
            .NonLazy();
        Container.Bind<CreateMainCameraCommand>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<TickableManager.TickableManager>()
            .FromComponentInNewPrefabResource(ResourcesConst.TickableManager)
            .AsSingle()
            .NonLazy();
        Container.Bind<CreateTickableManagerCommand>()
            .AsSingle()
            .NonLazy();

        Container.Bind<ApplicationStartup>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();

    }
    private void UIBindings()
    {
        Container.Bind<UIRoot>()
            .FromComponentInNewPrefabResource(ResourcesConst.UIRoot)
            .AsSingle();
        
        Container.Bind<UIService>()
            .AsSingle()
            .NonLazy();

       
        Container.Bind<UIMainMenuController>()
            .AsSingle()
            .NonLazy();
        Container.Bind<UIGameWindowController>()
            .AsSingle()
            .NonLazy();
        Container.Bind<UIEndGameWindowController>()
            .AsSingle()
            .NonLazy();
        Container.Bind<HUDWindowController>()
            .AsSingle()
            .NonLazy();
    }

    private void PlayerBinding()
    {
        Container.Bind<PlayerView>()
                    .FromComponentInNewPrefabResource(ResourcesConst.PlayerPrefab)
                    .AsSingle().NonLazy();
        Container.Bind<PlayerStorage>().AsSingle().Lazy();
        
        Container.BindFactory<PlayerView, Player, Player.Factory>();
        Container
            .Bind<PlayerConfig>()
            .FromScriptableObjectResource(ResourcesConst.PlayerConfig)
            .AsSingle()
            .NonLazy();
    }
    
    /*private void FallObjectPoolBinding()
    {
        Container
            .BindInterfacesAndSelfTo<FallObjectController>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindMemoryPool<FallObject.FallObject, FallObject.FallObject.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefabResource("Food")
            .UnderTransformGroup("Foods");
    }*/
}