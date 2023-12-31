using PlayerSpace;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using Zenject;

public class GameController: ITickable
{
    private  UnityEngine.Camera _camera;
    
    private FallObjectSpawner _spawner;
    private PlayerController _playerController;
    private UIService _uiService;
    private UIGameWindowController _gameWindowController;
    private HUDWindowController _hudWindowController;
    private ScoreCounter _scoreCounter;
    private SoundController _soundController;
    private readonly TickableManager _tickableManager;
    
    public GameController(PlayerController playerController,
        FallObjectSpawner spawner, 
        ScoreCounter scoreCounter,
        SoundController soundController,
        UnityEngine.Camera camera, 
        UIService uiService, 
        HUDWindowController hudWindowController,
        TickableManager tickableManager,
        UIGameWindowController gameWindowController)
    {
        _gameWindowController = gameWindowController;
        _tickableManager = tickableManager;
        _soundController = soundController;
        _camera = camera;
        _uiService = uiService;
        _hudWindowController = hudWindowController;
        _scoreCounter = scoreCounter;
        _spawner = spawner;

        ScoreInit();
        
        _playerController = playerController;

        _playerController.PlayerHpController.OnZeroHealth += StopGame;
        InitGame();
    }


    private void ScoreInit()
    {
        _scoreCounter.ScoreChangeNotify += _hudWindowController.ChangeScore;
    }

    public void InitGame()
    {
        _uiService.Show<UIMainMenuWindow>();
        
        _soundController.Play(SoundName.BackStart, loop:true);
    }

    public void StartGame()
    {
        _soundController.Stop();
        _soundController.Play(SoundName.BackMain, loop:true);
        
        _playerController.Spawn();
        _spawner.StartSpawn();
        _tickableManager.Add(this);
    }

    public void StopGame()
    {
        _playerController.DestroyView(()=>_gameWindowController.ShowEndMenuWindow());
        _spawner.StopSpawn();
        
        _tickableManager.Remove(this);
    }

    public void Tick()
    { }
}
