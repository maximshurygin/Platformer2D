using System;
using System.Collections;
using Cinemachine;
using Health;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : ObjectHealth
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _hurtDuration = 0.3f;
        [SerializeField] private float _respawnDuration = 2f;
        private WaitForSeconds _waitForHurtDuration;
        private WaitForSeconds _waitForRespawnDuration;
        private Coroutine _hurtCoroutine;
        private Coroutine _deathCoroutine;
        public event Action OnDeath;
        public event Action OnHealthChanged;
        


        private void Start()
        {
            _waitForHurtDuration = new WaitForSeconds(_hurtDuration);
            _waitForRespawnDuration = new WaitForSeconds(_respawnDuration);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            OnHealthChanged?.Invoke();
        }

        public override void TakeDamage(float damage)
        {
            _impulseSource.GenerateImpulse();
            base.TakeDamage(damage);
            OnHealthChanged?.Invoke();
            if (_currentHealth > 0)
            {
                _animator.SetTrigger("Hurt");
                _playerController.IsAttacking = false;
                _playerController.IsHurt = true;
                if (_hurtCoroutine != null)
                    StopCoroutine(_hurtCoroutine);

                _hurtCoroutine = StartCoroutine(HurtEnd());
            }
            else
            {
                _playerController.enabled = false;
                _rigidbody.velocity = Vector2.zero;
            	OnDeath?.Invoke();
            }
        }

        public void TakeHeal(float amount)
        { 
            _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        }

        private IEnumerator HurtEnd() 
        {
            yield return _waitForHurtDuration;
            _playerController.IsHurt = false;
            _hurtCoroutine = null;
        }
    }
}