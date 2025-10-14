using System.Collections;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _damage = 5f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _timeToHide = 5f;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _direction;

        public float Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                _spriteRenderer.flipX = Direction > 0;
            }
        }

        private void OnEnable()
        {
            StartCoroutine(CheckTimeToHide());
        }
        
        private void Update()
        {
            transform.position += new Vector3(_direction * _speed * Time.deltaTime, 0, 0);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.TryGetComponent(out PlayerHealth player);
                player.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }

        private IEnumerator CheckTimeToHide()
        {
            yield return new WaitForSeconds(_timeToHide);
            gameObject.SetActive(false);
        }
    }
}