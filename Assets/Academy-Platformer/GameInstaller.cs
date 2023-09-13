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
        SoundBinding();
        
        Container
            .Bind<ScoreCounter>()
            .AsSingle()
            .NonLazy();
        Container
            .Bind<InputController>()
            .AsSingle()
            .NonLazy();

        PlayerBinding();
        
        FallObjectBinding();
        
        Container
            .Bind<GameController>()
            .AsSingle()
            .NonLazy();
        
    }

    private void ApplicationStartupBindings()
    {
        Container
            .BindInterfacesAndSelfTo<UnityEngine.Camera>()
            .FromComponentInNewPrefabResource(ResourcesConst.MainCamera)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<CreateMainCameraCommand>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesAndSelfTo<TickableManager.TickableManager>()
            .FromComponentInNewPrefabResource(ResourcesConst.TickableManager)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<CreateTickableManagerCommand>()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<ApplicationStartup>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();

    }
    private void UIBindings()
    {
        Container
            .Bind<UIRoot>()
            .FromComponentInNewPrefabResource(ResourcesConst.UIRoot)
            .AsSingle();
        
        Container
            .Bind<UIService>()
            .AsSingle()
            .NonLazy();

       
        Container
            .Bind<UIMainMenuController>()
            .AsSingle()
            .NonLazy();
        Container
            .Bind<UIGameWindowController>()
            .AsSingle()
            .NonLazy();
        Container
            .Bind<UIEndGameWindowController>()
            .AsSingle()
            .NonLazy();
        Container
            .Bind<HUDWindowController>()
            .AsSingle()
            .NonLazy();
    }

    private void PlayerBinding()
    {
        Container
            .Bind<PlayerConfig>()
            .FromScriptableObjectResource(ResourcesConst.PlayerConfig)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<PlayerController>()
            .AsSingle()
            .NonLazy();
        Container
            .BindFactory<PlayerView, PlayerView.Factory>()
            .FromComponentInNewPrefabResource(ResourcesConst.PlayerPrefab);
    }
    
    private void FallObjectBinding()
    {
        Container
            .Bind<FallObjectConfig>()
            .FromResources(ResourcesConst.FallObjectConfigPath)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<FallObjectSpawnConfig>()
            .FromResource(ResourcesConst.FallObjectSpawnConfig)
            .AsSingle()
            .NonLazy();


        Container
            .BindMemoryPool<FallObjectView, FallObjectView.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefabResource(ResourcesConst.FallObjectViewPath)
            .UnderTransformGroup("FallObjects");
    }

    private void SoundBinding()
    {
        Container
            .Bind<SoundConfig>()
            .FromResources(ResourcesConst.SoundConfig)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<SoundController>()
            .AsSingle()
            .NonLazy();
        Container
            .BindMemoryPool<SoundView, SoundView.Pool>()
            .FromComponentInNewPrefabResource(ResourcesConst.SoundView)
            .UnderTransformGroup("Sounds");
    }
}