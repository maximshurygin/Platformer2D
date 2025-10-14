using Player;
using UnityEngine;
using Zenject;

namespace Loot
{
    public class Heart: Loot
    {
        [SerializeField] private float _health = 20f;
        
        private PlayerHealth _playerHealth;
        
        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void PickUp()
        {
            base.PickUp();
            _playerHealth.TakeHeal(_health);
        }
    }
}