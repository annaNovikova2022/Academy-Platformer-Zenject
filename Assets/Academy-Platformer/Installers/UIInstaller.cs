using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using Zenject;

public class UIInstaller : Installer<UIInstaller>
{
    public override void InstallBindings()
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
}