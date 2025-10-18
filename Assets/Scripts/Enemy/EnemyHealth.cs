using System;
using Health;
using Spawners;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyHealth : ObjectHealth
    {
        private KeySpawner _keySpawner;
        
        [Inject]
        private void Construct(KeySpawner keySpawner)
        {
            _keySpawner = keySpawner;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (_currentHealth <= 0)
            {
                _keySpawner.SpawnObject(transform.position);
            }
        }
    }
}