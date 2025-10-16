using System;
using System.Collections;
using UnityEngine;

namespace Spawners
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _requiredDistance = 25f;
        [SerializeField] private float _waitTime = 1f;
        private WaitForSeconds _waitTimer;
        private bool _hasSpawned;
        private float _distance;
        public event Action<SpawnPoint> OnPlayerDetected;
        
        public Transform Player => _player;

        
        private void Awake()
        {
            _waitTimer = new WaitForSeconds(_waitTime);
        }

        private void OnEnable()
        {
            StartCoroutine(CheckDistanceToPlayer());
        }

        private IEnumerator CheckDistanceToPlayer()
        {
            while (!_hasSpawned)
            {
                Debug.Log("Checking distance to player");

                _distance = Vector3.Distance(_player.position, transform.position);
                if (_distance <= _requiredDistance && OnPlayerDetected != null)
                {
                    OnPlayerDetected?.Invoke(this);
                    _hasSpawned = true;
                }
                yield return _waitTimer;
            }
        }
    }
}