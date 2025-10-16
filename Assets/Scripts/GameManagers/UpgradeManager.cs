using System.Collections.Generic;
using UnityEngine;
using Upgrades;
using Zenject;

namespace GameManagers
{
    public class UpgradeManager : MonoBehaviour
    {
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
            }
            else
            {
                _upgradeWindow.SetActive(true);
            }
        }
    }
}