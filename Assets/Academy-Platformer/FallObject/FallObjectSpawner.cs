using System;
using System.Collections.Generic;
using FallObject;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FallObjectSpawner: ITickable
{
    public FallObjectView.Pool Pool => _pool;

    private  ScoreCounter _scoreCounter;
    private  FallObjectView.Pool _pool;
    private readonly Zenject.TickableManager _tickableManager;
    private  float _spawnPeriodMin;
    private  float _spawnPeriodMax;
    private  float _minPositionX;
    private  float _maxPositionX;
    private  float _positionY;
    private  float _delayStartSpawn;
    private Vector3 _spawnPosition;
    private float _spawnPeriod;
    private float _timer;
    private int _typesCount;
    private FallObjectConfig _fallObjectConfig;
    [NonSerialized]private Dictionary<FallObjectView, FallObjectController> _fallsObjects = new Dictionary<FallObjectView, FallObjectController>();
    
    public  FallObjectSpawner(FallObjectSpawnConfig fallObjectSpawnConfig, 
        FallObjectView.Pool pool,
        FallObjectConfig fallObjectConfig,
        Zenject.TickableManager tickableManager)
    {
        _tickableManager = tickableManager;
        
        _fallObjectConfig = fallObjectConfig;
        
        var spawnerConfig = fallObjectSpawnConfig;
        _positionY = spawnerConfig.PositionY;
        _minPositionX = spawnerConfig.MinPositionX;
        _maxPositionX = spawnerConfig.MaxPositionX;
        _spawnPeriodMin = spawnerConfig.SpawnPeriodMin;
        _spawnPeriodMax = spawnerConfig.SpawnPeriodMax;
        _delayStartSpawn = spawnerConfig.DelayStartSpawn;
        _spawnPosition = new Vector2(Random.Range(_minPositionX, _maxPositionX), _positionY);

        _pool = pool;
        _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
        _typesCount = Enum.GetValues(typeof(FallObjectType)).Length;

    }

    public void StartSpawn()
    {
        _spawnPeriod = 6.5f;
        
        _tickableManager.Add(this);
    }

    public void StopSpawn() //!!!
    {
        _tickableManager.Remove(this);

        foreach (var view in _fallsObjects.Keys)
        {
            if (view.gameObject.activeSelf)
            {
                _pool.Despawn(view);
            }
        }
            
        _fallsObjects.Clear();
    }

    public void Tick()
    {
        _spawnPeriod -= Time.deltaTime;
        _timer += Time.deltaTime;
        
        if (_timer > _delayStartSpawn)
        {
            if (_spawnPeriod <= 0)
            {
                SpawnNewObject();
                _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
            }
        }
    }

    private void SpawnNewObject()
    {
        var type = Random.Range(0, _typesCount);
        var _model = _fallObjectConfig.Get((FallObjectType)type);
        
        var newObject = _pool.Spawn(_model.ObjectSprite);
        var newController = new FallObjectController(newObject, _model, _tickableManager);
        _fallsObjects.Add(newObject, newController);
        newController.SetActive(true);

        _spawnPosition.x = Random.Range(_minPositionX, _maxPositionX);
        newObject.gameObject.transform.position = _spawnPosition;
    }
    
}