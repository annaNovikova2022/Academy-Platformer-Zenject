using System;
using DG.Tweening;
using Sounds;
using UI.HUD;
using Zenject;

namespace PlayerSpace
{
    public class PlayerController
    {
        public Action OnDisposed;
        public event Action<float> OnChangeSpeed;

        public PlayerHpController PlayerHpController => _playerHpController;
        
        public const float DelayDestroyPlayer = 2f;

        private SoundController _soundController;
        private InputController _inputController;
        private PlayerConfig _playerConfig;
        [NonSerialized] private PlayerView _playerView;
        private PlayerHpController _playerHpController;
        private PlayerView.Factory _factoryPlayer;
        private PlayerMovementController _playerMovementController;
        private PlayerAnimator _playerAnimator;
        private UnityEngine.Camera _camera;
        
        private float _currentHealth;
        private float _currentSpeed;

        public PlayerController(
            InputController inputController,
            HUDWindowController hudWindowController,
            UnityEngine.Camera camera,
            SoundController soundController,
            PlayerView.Factory factoryPlayer,
            PlayerConfig playerConfig)
        {
            _soundController = soundController;
             _inputController = inputController;
             _camera = camera;
            _playerConfig = playerConfig;
            _factoryPlayer = factoryPlayer;
            
            _playerHpController = new PlayerHpController(_playerConfig.PlayerModel.Health, _soundController);
            _playerHpController.OnHealthChanged += hudWindowController.ChangeHealthPoint;
        }
        
        public PlayerView Spawn()
        {
            var model = _playerConfig.PlayerModel;
            _currentHealth = model.Health;
            _currentSpeed = model.Speed;
            
            _playerView = _factoryPlayer.Create();
            
            _playerAnimator = new PlayerAnimator(_playerView, _camera);
            _playerAnimator.Spawn();
            
            _playerMovementController = new PlayerMovementController(_inputController, _playerView, this);

            _playerHpController.SetHealth(_playerConfig.PlayerModel.Health);
            //_playerScoreCounter.SetScores();
            
            return _playerView;
        }

        public void SetSpeed(float newSpeed)
        {
            _currentSpeed = newSpeed;

            OnChangeSpeed?.Invoke(_currentSpeed);
        }

        public void DestroyView(TweenCallback setEndWindow = null)
        {
            OnDisposed?.Invoke();
            
            _soundController.Stop();
            _soundController.Play(SoundName.GameOver);

            _playerAnimator.Death(setEndWindow);

            GameObjectContext.Destroy(_playerView.gameObject, DelayDestroyPlayer);
            _playerView = null;
        }
    }
}