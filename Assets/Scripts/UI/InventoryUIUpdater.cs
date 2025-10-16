using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;
using Zenject;

namespace UI
{
    public class InventoryUIUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _keyText;
        [SerializeField] private GameObject _fireResistWindow;
        [SerializeField] private GameObject _electricResistWindow;
        private PlayerData _playerData;
        private PlayerUpgrade _playerUpgrade;

        [Inject]
        private void Construct(PlayerData playerData, PlayerUpgrade playerUpgrade)
        {
            _playerData = playerData;
            _playerUpgrade = playerUpgrade;
        }

        private void OnEnable()
        {
            _playerData.OnCoinsChanged += CoinsChanged;
            _playerData.OnKeysChanged += KeysChanged;
            _playerUpgrade.OnUpgradeAdded +=  OnUpgradeAdded;
        }

        private void OnDisable()
        {
            _playerData.OnCoinsChanged -= CoinsChanged;
            _playerData.OnKeysChanged -= KeysChanged;
            _playerUpgrade.OnUpgradeAdded -=  OnUpgradeAdded;
        }

        private void CoinsChanged(int coins)
        {
            _coinText.text = coins.ToString();
        }
        
        private void KeysChanged(int keys)
        {
            _keyText.text = keys.ToString();
        }

        private void OnUpgradeAdded(UpgradeCard upgrade)
        {
            switch (upgrade.UpgradeType)
            {
                case UpgradeType.FireResist:
                    _fireResistWindow.SetActive(true);
                    break;
                case UpgradeType.ElectricResist:
                    _electricResistWindow.SetActive(true);
                    break;
            }
        }
    }
}