using PlayerSpace;
using Zenject;

public class PlayerInstaller : Installer<PlayerInstaller>
{
    public override void InstallBindings()
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
            .FromComponentInNewPrefabResource(ResourcesConst.PlayerPrefab); ///Это работает, тк нет nonLazy. Объект создается в любом месте, когда это необходимо.
    }
}