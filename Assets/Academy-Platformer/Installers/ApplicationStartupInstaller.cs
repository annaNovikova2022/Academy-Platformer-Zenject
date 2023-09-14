using Camera;
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
            .Bind<ApplicationStartup>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }
}