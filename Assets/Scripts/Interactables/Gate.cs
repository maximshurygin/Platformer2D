using System.Collections;
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
        [SerializeField] private float _delay = 0.5f;
        private PlayerData _playerData;
        private HintManager _hintManager;
        private bool _isOpen;
        private string _hintText = "You need a key";

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
                    StartCoroutine(DisableCollider());
                    _isOpen = true;
                }
                else
                {
                    _hintManager.ShowHint(_hintText);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _hintManager.HideHint();
            }
        }

        private IEnumerator DisableCollider()
        {
            yield return new WaitForSeconds(_delay);
            _collider.enabled = false;
        }
    }
}