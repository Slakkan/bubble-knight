using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private int _enemysToSpawn = 50;

    [SerializeField] private Transform[] _spawnPlanes;

    [SerializeField] private float _timeBetweenSpawns = 1f;

    [SerializeField] private ScoreController _scoreController;

    private float _timeSinceLastSpawn = 0f;
    private int _totalSpawned = 0;
    private void Update()
    {
        if (_timeSinceLastSpawn > _timeBetweenSpawns)
        {
            Spawn();
            _timeSinceLastSpawn = 0f;
        }

        _timeSinceLastSpawn += Time.deltaTime;
    }

    public void Spawn()
    {
        if (_totalSpawned >= _enemysToSpawn)
        {
            return;
        }
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
        Enemy obj = Instantiate(_enemyPrefab, new Vector3(x_rand, 0, z_rand), Quaternion.identity, transform);
        obj.Init(_playerTransform, _scoreController);
        _totalSpawned++;
    }
}