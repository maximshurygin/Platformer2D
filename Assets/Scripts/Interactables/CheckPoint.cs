using GameManagers;
using Player;
using UnityEngine;
using Zenject;

namespace Interactables
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;
        private PlayerData _playerData;
        private HintManager _hintManager;
        private bool _isActivated;

        [Inject]
        private void Construct(PlayerData playerData, HintManager hintManager)
        {
            _playerData = playerData;
            _hintManager = hintManager;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isActivated) return;
            
            if (other.CompareTag("Player"))
            {
                _audioSource.Play();
                _animator.SetBool("Activated", true);
                _isActivated = true;
                _playerData.SetLastCheckpoint(transform);
                _hintManager.ShowAndHideHint("CheckPoint activated");
            }
        }
    }
}