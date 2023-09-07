using System;
using FallObject;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FallObjectSpawner: ITickable
{
    public FallObjectPool Pool => _pool;

    private  ScoreCounter _scoreCounter;
    private  FallObjectPool _pool;
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

    [Inject]
    public void Construct(ScoreCounter scoreCounter)
    {
        var spawnerConfig = Resources.Load<FallObjectSpawnConfig>(ResourcesConst.FallObjectSpawnConfig);
        _positionY = spawnerConfig.PositionY;
        _minPositionX = spawnerConfig.MinPositionX;
        _maxPositionX = spawnerConfig.MaxPositionX;
        _spawnPeriodMin = spawnerConfig.SpawnPeriodMin;
        _spawnPeriodMax = spawnerConfig.SpawnPeriodMax;
        _delayStartSpawn = spawnerConfig.DelayStartSpawn;
        _spawnPosition = new Vector2(Random.Range(_minPositionX, _maxPositionX), _positionY);

        _pool = new FallObjectPool(new FallObjectFactory(), scoreCounter);
        _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
        _typesCount = Enum.GetValues(typeof(FallObjectType)).Length;
    }

    public void StartSpawn()
    {
        _spawnPeriod = 6.5f;
        TickableManager.TickableManager.UpdateNotify += Tick;
    }

    public void StopSpawn()
    {
        TickableManager.TickableManager.UpdateNotify -= Tick;
        Pool.AllReturnToPool();
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
        var newObject = _pool.CreateObject((FallObjectType)type);
        _spawnPosition.x = Random.Range(_minPositionX, _maxPositionX);
        newObject.gameObject.transform.position = _spawnPosition;
    }
}