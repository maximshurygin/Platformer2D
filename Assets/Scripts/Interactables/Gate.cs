using GameManagers;
using Player;
using UnityEngine;
using Zenject;

namespace Interactables
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Animator _animatorUpper;
        [SerializeField] private Animator _animatorLower;
        private PlayerData _playerData;
        private HintManager _hintManager;
        private bool _isOpen;
        private string _hintText = "I need a key";

        [Inject]
        private void Construct(PlayerData playerData, HintManager hintManager)
        {
            _playerData = playerData;
            _hintManager = hintManager;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isOpen) return;
            
            if (other.CompareTag("Player"))
            {
                if (_playerData.Keys > 0)
                {
                    _playerData.UseKey();
                    _animatorUpper.SetBool("Opened", true);
                    _animatorLower.SetBool("Opened", true);
                    _collider.enabled = false;
                    _isOpen = true;
                    Debug.Log("Gate opened");
                    Debug.Log($"_playerData.Keys: {_playerData.Keys}");
                }
                else
                {
                    _hintManager.ShowHint(_hintText);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _hintManager.HideHint();
        }
    }
}