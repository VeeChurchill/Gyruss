using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _minAmount = 2;
    [SerializeField] private int _maxAmount = 10;
    [SerializeField] private float _timeBetweenWavesInSeconds = 2f;
    [SerializeField] private float _spawnRadius = 2f;
    [SerializeField] private List<EnemyController> _enemyPrefabs = new List<EnemyController>();
    [SerializeField] private float _timeBetweenSpawnsInWaveInSeconds = 0.5f;
    [SerializeField] private UiController _uiController;

    private float _timeSinceLastSpawn = 0;
    private Coroutine _spawnCoroutine;

    // Update is called once per frame
    void FixedUpdate()
    {
        _timeSinceLastSpawn += Time.fixedDeltaTime;

        if(_timeSinceLastSpawn >= _timeBetweenWavesInSeconds && _spawnCoroutine == null)
        {
            _spawnCoroutine = StartCoroutine(SpawnEnemyWave());
        }
    }

    private IEnumerator SpawnEnemyWave()
    {
        int amountToSpawn = Random.Range(_minAmount, _maxAmount + 1);
        float angleToSpawn = Random.Range(0f, 360f);
        
        while (amountToSpawn > 0)
        {
            //Select a random enemy to spawn
            var index = Random.Range(0, _enemyPrefabs.Count);

            var enemy = Instantiate(_enemyPrefabs[index]);
            enemy.Initialize(transform.position, _spawnRadius, angleToSpawn, (reward) => _uiController.ChangeScore(reward));
            amountToSpawn--;

            // Wait until next spawn in wave
            yield return new WaitForSeconds(_timeBetweenSpawnsInWaveInSeconds);
        }
        _timeSinceLastSpawn = 0;
        _spawnCoroutine = null;
        yield return null;
    }
}
