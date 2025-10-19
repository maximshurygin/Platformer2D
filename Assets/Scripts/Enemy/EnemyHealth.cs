using Health;
using Spawners;
using Zenject;

namespace Enemy
{
    public class EnemyHealth : ObjectHealth
    {
        private KeySpawner _keySpawner;
        private bool _hasSpawnedKey;
        
        
        [Inject]
        private void Construct(KeySpawner keySpawner)
        {
            _keySpawner = keySpawner;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _hasSpawnedKey = false;
        }
        

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (_currentHealth <= 0 && !_hasSpawnedKey)
            {
                _hasSpawnedKey = true;
                _keySpawner.SpawnObject(transform.position);
            }
        }
    }
}