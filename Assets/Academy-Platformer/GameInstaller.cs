using Camera;
using TickableManager;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var camera = Container.InstantiatePrefabResourceForComponent<UnityEngine.Camera>(ResourcesConst.MainCamera);
        Container.BindInterfacesAndSelfTo<UnityEngine.Camera>().FromInstance(camera).AsSingle().NonLazy();
        Container.Bind<CreateMainCameraCommand>().AsSingle().NonLazy();
        
        var tickable = Container.InstantiatePrefabResourceForComponent<TickableManager.TickableManager>(ResourcesConst.TickableManager);
        Container.BindInterfacesAndSelfTo<TickableManager.TickableManager>().FromInstance(tickable).AsSingle().NonLazy();
        Container.Bind<CreateTickableManagerCommand>().AsSingle().NonLazy();

        Container.Bind<ApplicationStartup>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.Bind<GameController>().AsSingle().NonLazy();
    }
}