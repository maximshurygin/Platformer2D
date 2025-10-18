using System;
using System.Collections;
using UnityEngine;
using Weapon;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckDistance = 2f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _attackRange = 4f;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ObjectPool.ObjectPool _objectPool;
        [SerializeField] private float _attackInterval = 2f;
        // [SerializeField] private float _waitToHideTime = 3f;
        // [SerializeField] private float _disanceToHide = 20f;
        [SerializeField] private float _deactivationRange = 10f;
        private Transform _player;
        private Transform _bulletContainer;
        private WaitForSeconds _attackTimer;
        private float _direction = 1;
        private bool _isPatrolling = true;
        // private WaitForSeconds _waitToHideTimer;

        private void Start()
        {
            _attackTimer = new WaitForSeconds(_attackInterval);
            // _waitToHideTimer = new WaitForSeconds(_waitToHideTime);
            
        }

        private void OnEnable()
        {
            StartCoroutine(Attack());
            _isPatrolling = true;
            
            // StartCoroutine(CheckDistanceToHide());

        }

        private void FixedUpdate()
        {
            Patrol();
        }

        private void Patrol()
        {
            if (!_isPatrolling || !CheckDistanceToPlayer(_deactivationRange)) return;
            CheckGround();
            _rb.velocity = new Vector2(_direction * _speed, _rb.velocity.y);
            _spriteRenderer.flipX = _direction > 0;
        }

        private void CheckGround()
        {
            Vector2 rayDirection = new Vector2(_direction, -1).normalized;
            RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, rayDirection, _groundCheckDistance, _groundLayer);
            Debug.DrawRay(_groundCheck.position, rayDirection * _groundCheckDistance, Color.green);

            if (hit.collider ==null)
            {
                _direction = -_direction;
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                if (!CheckDistanceToPlayer(_attackRange))
                {
                    _isPatrolling = true;
                    yield return null;
                    continue;
                }
                _isPatrolling = false;

                _direction = transform.position.x > _player.position.x ? -1 : 1;
                _spriteRenderer.flipX = _direction > 0;
                
                GameObject projectileObj = _objectPool.Get();
                Projectile projectile = projectileObj.GetComponent<Projectile>();
                projectile.transform.position = _shootPoint.position;
                projectile.transform.SetParent(_bulletContainer);
                projectile.Direction = _direction;
                _animator.SetTrigger("Attack");
                yield return _attackTimer;
            }
        }
        
        private bool CheckDistanceToPlayer(float range)
        {
            if (_player == null) return false;
            float distance = (_player.position - transform.position).magnitude;
            if (distance <= range)
            {
                return true;
            }
            return false;
        }
        
        public void SetDetails(Transform player, Transform bulletContainer)
        {
            _player = player;
            _bulletContainer = bulletContainer;
        }
    }
}