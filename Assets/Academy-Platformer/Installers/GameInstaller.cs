using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        ApplicationStartupInstaller.Install(Container);
        UIInstaller.Install(Container);
        SoundInstaller.Install(Container);

        Container
            .Bind<ScoreCounter>()
            .AsSingle()
            .NonLazy();
        /*Container
            .Bind<InputController>()
            .AsSingle()
            .NonLazy();*/

        PlayerInstaller.Install(Container);
        FallObjectInstaller.Install(Container);

        Container
            .Bind<GameController>()
            .AsSingle()
            .NonLazy();

    }
}