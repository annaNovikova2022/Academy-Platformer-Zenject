using System;
using PlayerSpace;
using UnityEngine;
using Zenject;

namespace FallObject
{
    public class FallObjectController: IFixedTickable
    {
        public static event Action<float> DamageToPlayerNotify;
        public event Action<FallObjectController> DeathAnimationEndedNotify;
        public event Action<FallObjectController> ObjectFellNotify;
        public int PointsPerObject => _pointsPerObject;
        public FallObjectView View => _view;
        public int Damage => _damage;

        private Vector3 _defaultScale = new Vector3(0.15f, 0.15f, 0.15f);
        private Vector3 _deltaVector = new Vector3(0, -0.001f, 0);
        private FallObjectAnimator _animator;
        private FallObjectView _view;
        private FallObjectModel _model;
        private  FallObjectConfig _objectConfig;
        private int _pointsPerObject;
        private float _minPositionY = -7f;
        private float _fallSpeed;
        private int _damage;
        private bool _isCatched;
        private readonly TickableManager _tickableManager;

        public FallObjectController(FallObjectView view,
            FallObjectModel model,
            TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
            _model = model;
                
            _view = view;
            _view.transform.localScale = _defaultScale;
            SetModel(_model);
            
            _animator = new FallObjectAnimator(_view);
            _animator.Spawn();
            _animator.DeathAnimationEnded += () => DeathAnimationEndedNotify?.Invoke(this);
            
            _view.OnCollisionEnter2DNotify += OnCollisionEnter2D;
        }

        void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (!_isCatched)
            {
                var player = collision2D.gameObject.GetComponent<PlayerView>();

                if (player != null)
                {
                    _animator.Death();
                    _isCatched = true;
                    SetActive(false);
                }
            }
        }

        public void SetActive(bool value)
        {
            if (value == true)
            {
                _tickableManager.AddFixed(this);
                
            }
            else
            {
                _tickableManager.RemoveFixed(this);
            }

            _view.transform.localScale = _defaultScale;
            View.gameObject.SetActive(value);
            _isCatched = !value;
        }

        public void SetModel(FallObjectModel model)
        {
            _pointsPerObject = model.PointsPerObject;
            _fallSpeed = model.FallSpeed;
            _damage = model.Damage;
            _view.SpriteRenderer.sprite = model.ObjectSprite;
        }

        public void FixedTick()
        {
            if (_view.transform.position.y <= _minPositionY)
            {
                ObjectFellNotify?.Invoke(this);
                DamageToPlayerNotify?.Invoke(_damage);
            }

            _view.transform.position += _deltaVector * _fallSpeed;
        }
    }
}