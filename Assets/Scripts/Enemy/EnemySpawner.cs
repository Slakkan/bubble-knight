using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _moskitoPrefab;
    [SerializeField] private Enemy _seaUrchinPrefab;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private int _moskitosToSpawn = 0;
    [SerializeField] private int _seaUrchinsToSpawn = 0;

    [SerializeField] private Transform[] _spawnPlanes;

    [SerializeField] private float _timeBetweenSpawns = 1f;

    [SerializeField] private ScoreController _scoreController;

    private float _timeSinceLastSpawn = 0f;
    private int _totalSpawned = 0;
    private float _timeTillNextSpawn = 0;

    public float CurrentTimeTillNextSpawn => _timeTillNextSpawn - _timeSinceLastSpawn;

    [SerializeField]
    private SoWave[] _waves;

    private int currentWave;
    private float _lastWaveTime = 0f;

    private void Start()
    {
        InitializeWave(0);
        Spawn();
        _timeSinceLastSpawn = 0f;
    }

    private void InitializeWave(int index)
    {
        int actualWave = currentWave;
        bool repeatLastWave = false;
        if (currentWave >= _waves.Length)
        {
            repeatLastWave = true;
            actualWave = _waves.Length-1;
        }
        SoWave currentWaveData = _waves[actualWave];
        _moskitosToSpawn = currentWaveData.AmountOfMoskitos;
        _seaUrchinsToSpawn = currentWaveData.AmountOfSeaUrchin;
        if (repeatLastWave)
        {
            _timeTillNextSpawn = _lastWaveTime * 0.95f;
        }
        else
        {
            _timeTillNextSpawn = currentWaveData.TimeBeforeNextWave;
        }
        _lastWaveTime = _timeTillNextSpawn;
    }

    private void Update()
    {
        if (_timeSinceLastSpawn > _timeTillNextSpawn)
        {
            currentWave++;
            
            InitializeWave(currentWave);
            Spawn();
            _timeSinceLastSpawn = 0f;
        }

        _timeSinceLastSpawn += Time.deltaTime;
    }

    public void Spawn()
    {
        for (int i = 0; i < _moskitosToSpawn; i++)
        {
            SpawnPrefab(_moskitoPrefab);
        }
        for (int i = 0; i < _seaUrchinsToSpawn; i++)
        {
            SpawnPrefab(_seaUrchinPrefab);
        }
    }

    private void SpawnPrefab(Enemy prefab)
    {
        int planeToSpawn = Random.Range(0, _spawnPlanes.Length);
        /* Move the object to where you want withing in the dimensions of the plane */
        // random the x and z position between bounds
        Bounds planeBounds = _spawnPlanes[planeToSpawn].GetComponent<MeshRenderer>().bounds;

        float x_rand = Random.Range(planeBounds.center.x - planeBounds.size.x * 0.5f,
            planeBounds.center.x + planeBounds.size.x * 0.5f);
        float z_rand = Random.Range(planeBounds.center.z - planeBounds.size.z * 0.5f,
            planeBounds.center.z + planeBounds.size.z * 0.5f);

        // Random the y position from the smallest bewteen x and z
        //z_rand = x_rand > z_rand ? Random.Range(0, z_rand) : Random.Range(0, x_rand);

        // Spawn the object as a child of the plane. This will solve any rotation issues
        Enemy obj = Instantiate(prefab, new Vector3(x_rand, 0, z_rand), Quaternion.identity, transform);
        obj.Init(_playerTransform, _scoreController);
    }
}