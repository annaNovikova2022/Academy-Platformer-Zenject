using PlayerSpace;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;

public class GameController
{
    private readonly UnityEngine.Camera _camera;
    
    private FallObjectSpawner _spawner;
    private PlayerController _playerController;
    private UIService _uiService;
    private UIGameWindowController _gameWindowController;
    private HUDWindowController _hudWindowController;
    private ScoreCounter _scoreCounter;
    private SoundController _soundController;
    
    public GameController(PlayerController playerController,FallObjectSpawner spawner, ScoreCounter scoreCounter,SoundController soundController,UnityEngine.Camera camera, UIService uiService, HUDWindowController hudWindowController)
    {
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
        TickableManager.TickableManager.UpdateNotify += Update;
    }

    public void StopGame()
    {
        _playerController.DestroyView(()=>_gameWindowController.ShowEndMenuWindow());
        _spawner.StopSpawn();
        TickableManager.TickableManager.UpdateNotify -= Update;
    }

    private void Update()
    { }
}
