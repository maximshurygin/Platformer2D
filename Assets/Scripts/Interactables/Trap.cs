using System.Collections;
using Player;
using UnityEngine;
using Upgrades;
using Zenject;

namespace Interactables
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private UpgradeCard _requiredUpgrade;
        [SerializeField] private float _normalDamage = 100f;
        [SerializeField] private float _damageWithUpgrade = 50f;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Vector2 _animatorDelayRange = new Vector2(0f, 0.5f);
        private PlayerUpgrade _playerUpgrade;
        private float _animatorDelay;

        [Inject]
        private void Construct(PlayerUpgrade playerUpgrade)
        {
            _playerUpgrade = playerUpgrade;
        }

        private void OnEnable()
        {
            _animatorDelay = Random.Range(_animatorDelayRange.x, _animatorDelayRange.y);
            StartCoroutine(LaunchAnimator());
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                if (CheckIfPlayerHasProtection())
                {
                    player.TakeDamage(_damageWithUpgrade);
                }
                else
                {
                    player.TakeDamage(_normalDamage);
                }
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

        private IEnumerator LaunchAnimator()
        {
            yield return new WaitForSeconds(_animatorDelay);
            _animator.enabled = true;
        }
    }
}