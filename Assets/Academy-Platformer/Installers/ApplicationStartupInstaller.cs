using Camera;
using TickableManager;
using Zenject;

public class ApplicationStartupInstaller : Installer<ApplicationStartupInstaller>
{
    public override void InstallBindings()
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
}