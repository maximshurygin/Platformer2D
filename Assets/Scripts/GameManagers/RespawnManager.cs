using System.Collections;
using Player;
using UnityEngine;
using Zenject;

namespace GameManagers
{
    public class RespawnManager : MonoBehaviour
    {
        [SerializeField] private float _respawnDelay = 2f;
        private PlayerData _playerData;
        private PlayerHealth _playerHealth;
		private PlayerController _playerController;
        private WaitForSeconds _waitForRespawn;
        private Coroutine _respawnCoroutine;

        [Inject]
        private void Construct(PlayerData playerData, PlayerHealth playerHealth, PlayerController playerController)
        {
            _playerData = playerData;
            _playerHealth = playerHealth;
			_playerController = playerController;
        }

        private void Start()
        {
            _waitForRespawn = new WaitForSeconds(_respawnDelay);
        }

        private void OnEnable()
        {
            _playerHealth.OnDeath += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _playerHealth.OnDeath -= OnPlayerDeath;
        }
        
        private void OnPlayerDeath()
        {
            if (_respawnCoroutine != null)
            {
                StopCoroutine(_respawnCoroutine);
            }
            _respawnCoroutine = StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return _waitForRespawn;
            _playerHealth.gameObject.transform.position = new Vector3(_playerData.LastCheckpoint.position.x + 1f, _playerData.LastCheckpoint.position.y + 1f, _playerData.LastCheckpoint.position.z);
            _playerHealth.gameObject.SetActive(true);
			_playerController.enabled = true;
            _respawnCoroutine = null;
        }
    }
}