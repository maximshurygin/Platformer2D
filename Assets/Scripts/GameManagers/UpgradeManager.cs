using UnityEngine;
using Zenject;

namespace GameManagers
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _upgradeWindow;
        private PlayerController _playerController;

        [Inject]
        private void Construct(PlayerController player)
        {
            _playerController = player;
        }
        
        private void OnEnable()
        {
            _playerController.OnInteract += ToggleWindow;
        }

        private void OnDisable()
        {
            _playerController.OnInteract -= ToggleWindow;
        }

        public void ToggleWindow()
        {
            if (_upgradeWindow.activeInHierarchy)
            {
                _upgradeWindow.SetActive(false);
                _audioSource.Play();
            }
            else
            {
                _upgradeWindow.SetActive(true);
                _audioSource.Play();
            }
        }
    }
}