using FallObject;
using Zenject;

public class FallObjectInstaller : Installer<FallObjectInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<FallObjectConfig>()
            .FromResource(ResourcesConst.FallObjectConfigPath)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<FallObjectSpawnConfig>()
            .FromResource(ResourcesConst.FallObjectSpawnConfig)
            .AsSingle();
        Container
            .Bind<FallObjectSpawner>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindMemoryPool<FallObjectView, FallObjectView.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefabResource(ResourcesConst.FallObjectViewPath)
            .UnderTransformGroup("FallObjects");
    }
}