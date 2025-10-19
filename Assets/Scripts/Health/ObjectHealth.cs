using System.Collections;
using Effects;
using UnityEngine;

namespace Health
{
    public class ObjectHealth : MonoBehaviour
    {
        [SerializeField] private AudioSource _deathAudioSource;
        [SerializeField] private AudioSource _impactAudioSource;
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _currentHealth;
        [SerializeField] private float _dieInterval = 0.5f;
        [SerializeField] protected Animator _animator;
        [SerializeField] private DamageFlash _damageFlash;
        private WaitForSeconds _dieTimer;
        private bool _isDead;

        public bool IsDead => _isDead;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;

        private void Awake()
        {
            _dieTimer = new WaitForSeconds(_dieInterval);
        }

        protected virtual void OnEnable()
        {
            _currentHealth = _maxHealth;
            _isDead = false;
        }
        

        public virtual void TakeDamage(float damage)
        {
            if (_isDead) return;
            
            _currentHealth = Mathf.Max(_currentHealth - damage, 0f);
            _damageFlash?.Flash();
            if (_currentHealth > 0f)
            {
                _impactAudioSource.Play();
            }
            else if (_currentHealth <= 0)
            {
                _isDead = true;
                AudioSource.PlayClipAtPoint(_deathAudioSource.clip, transform.position, 1f);
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            _animator.SetBool("IsDead", true);
            yield return _dieTimer;
            gameObject.SetActive(false);
        }
    }
}