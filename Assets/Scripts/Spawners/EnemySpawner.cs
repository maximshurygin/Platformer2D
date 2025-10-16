using Enemy;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] _spawnPoints;
        [SerializeField] private Transform _bulletContainer;
        [SerializeField] private ObjectPool.ObjectPool _objectPool;

        private void OnEnable()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.OnPlayerDetected += SpawnEnemy;
            }
        }

        private void OnDisable()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.OnPlayerDetected -= SpawnEnemy;
            }
        }

        public void SpawnEnemy(SpawnPoint spawnPoint)
        {
            Debug.Log("Spawning enemy");
            GameObject enemyObj = _objectPool.Get();
            enemyObj.transform.position = spawnPoint.transform.position;
            enemyObj.transform.SetParent(transform);

            if (enemyObj.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.SetDetails(spawnPoint.Player, _bulletContainer);
            }
        }


    }
}