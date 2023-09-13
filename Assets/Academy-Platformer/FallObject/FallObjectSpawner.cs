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
    private Dictionary<FallObjectView, FallObjectController> _fallsObjects = new Dictionary<FallObjectView, FallObjectController>();

    [Inject]
    public void Construct(FallObjectSpawnConfig fallObjectSpawnConfig, FallObjectView.Pool pool, ScoreCounter scoreCounter)
    {
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
        TickableManager.TickableManager.UpdateNotify += Tick;
    }

    public void StopSpawn() //!!!
    {
        TickableManager.TickableManager.UpdateNotify -= Tick;
        //Pool.AllReturnToPool();

        foreach (var view in _fallsObjects.Keys)
        {
            if (view.gameObject.activeInHierarchy)
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
        var newObject = _pool.Spawn();
        _fallsObjects.Add(newObject, new FallObjectController(newObject));
        _spawnPosition.x = Random.Range(_minPositionX, _maxPositionX);
        newObject.gameObject.transform.position = _spawnPosition;
    }
}