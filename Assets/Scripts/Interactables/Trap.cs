using System;
using System.Collections;
using Player;
using UnityEngine;
using Upgrades;
using Zenject;

namespace Interactables
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private UpgradeCard _requiredUpgrade;
        [SerializeField] private float _damage = 25f;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _renderer;
        private PlayerUpgrade _playerUpgrade;

        
        private WaitForSeconds _waitVisible;
        private WaitForSeconds _waitInvisible;

        [Inject]
        private void Construct(PlayerUpgrade playerUpgrade)
        {
            _playerUpgrade = playerUpgrade;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("OnTriggerEnter2D");
            if (other.TryGetComponent(out PlayerHealth player))
            {
                if (CheckIfPlayerHasProtection()) return;
                player.TakeDamage(_damage);
            }
        }
        
        public void ToggleCollider()
        {
            _collider.enabled = !_collider.enabled;
        }

        private bool CheckIfPlayerHasProtection()
        {
            return _playerUpgrade.PurchasedUpgradeCards.Contains(_requiredUpgrade);
        }
    }
}