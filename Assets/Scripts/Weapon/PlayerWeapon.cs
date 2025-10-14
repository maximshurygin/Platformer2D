using System.Collections;
using Enemy;
using UnityEngine;

namespace Weapon
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private Collider2D _weaponCollider;
        
        public void EnableCollider()
        {
            _weaponCollider.enabled = true;
        }
        
        public void DisableCollider()
        {
            _weaponCollider.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(_damage);
                _weaponCollider.enabled = false;
            }
        }
    }
}