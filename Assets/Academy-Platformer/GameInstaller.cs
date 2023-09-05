using Camera;
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
        
        Container.Bind<GameController>().AsSingle().NonLazy();
    }

    private void ApplicationStartupBindings()
    {
        var camera = Container.InstantiatePrefabResourceForComponent<UnityEngine.Camera>(ResourcesConst.MainCamera);
        Container.BindInterfacesAndSelfTo<UnityEngine.Camera>().FromInstance(camera).AsSingle().NonLazy();
        Container.Bind<CreateMainCameraCommand>().AsSingle().NonLazy();
        
        var tickable = Container.InstantiatePrefabResourceForComponent<TickableManager.TickableManager>(ResourcesConst.TickableManager);
        Container.BindInterfacesAndSelfTo<TickableManager.TickableManager>().FromInstance(tickable).AsSingle().NonLazy();
        Container.Bind<CreateTickableManagerCommand>().AsSingle().NonLazy();

        Container.Bind<ApplicationStartup>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

    }
    private void UIBindings()
    {
        Container.Bind<UIService>().AsSingle().NonLazy();

        Container.Bind<UIMainMenuController>().AsSingle().NonLazy();
        Container.Bind<UIGameWindowController>().AsSingle().NonLazy();
        Container.Bind<UIEndGameWindowController>().AsSingle().NonLazy();
        Container.Bind<HUDWindowController>().AsSingle().NonLazy();
    }

    private void FallObjectFactoryBinding()
    {
        
    }
}