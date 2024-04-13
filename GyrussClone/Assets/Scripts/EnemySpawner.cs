using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyPrefab = null;
        [SerializeField] private int _maxAmountEnemies = 10;
        [SerializeField] private int _minAmountEnemies = 2;
        [SerializeField] private float _timeBetweenSpawnsInWaveInSeconds = 0.5f;
        [SerializeField] private float _timeBetweenWavesInSeconds = 2f;
        [SerializeField] private UiController _uiController = null;

        private Coroutine _spawnCoroutine;
        private float _timeSinceLastSpawn;

        private void FixedUpdate()
        {
            _timeSinceLastSpawn += Time.fixedDeltaTime;

            if (_timeSinceLastSpawn >= _timeBetweenWavesInSeconds && _spawnCoroutine == null)
                _spawnCoroutine = StartCoroutine(SpawnEnemyWave());
        }

        private IEnumerator SpawnEnemyWave()
        {
            var amountToSpawn = Random.Range(_minAmountEnemies, _maxAmountEnemies + 1);

            while (amountToSpawn > 0)
            {
                var enemy = Instantiate(_enemyPrefab);
                enemy.Initialize(transform.position, reward => _uiController.ChangeScore(reward));
                amountToSpawn--;

                // Wait until next spawn in wave
                yield return new WaitForSeconds(_timeBetweenSpawnsInWaveInSeconds);
            }

            _timeSinceLastSpawn = 0;
            _spawnCoroutine = null;
            yield return null;
        }
    }
}