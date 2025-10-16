using GameManagers; 
using Player; 
using TMPro; 
using UnityEngine; 
using UnityEngine.UI; 
using Zenject;

namespace Upgrades
{
    public class UpgradeWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _upgradeCardWindow; 
        [SerializeField] private TextMeshProUGUI _titleText; 
        [SerializeField] private TextMeshProUGUI _cardName; 
        [SerializeField] private Image _cardIcon; 
        [SerializeField] private TextMeshProUGUI _cardDescription; 
        [SerializeField] private TextMeshProUGUI _cardCost; 
        [SerializeField] private Button _upgradeButton; 
        private PlayerUpgrade _playerUpgrade; 
        private HintManager _hintManager; 
        private UpgradeCard _upgradeCard; 
        private PlayerData _playerData;

        [Inject]
        private void Construct(PlayerUpgrade playerUpgrade, HintManager hintManager, PlayerData playerData)
        {
            _playerUpgrade = playerUpgrade; _hintManager = hintManager; _playerData = playerData;
        }

        private void OnEnable()
        {
            _playerData.NearComputer.OnOutOfRange += CloseUpgradeWindow; 
            _upgradeButton.onClick.AddListener(Upgrade); 
            _upgradeCard = _playerData.NearComputer.AvailableUpgradeCard;
            if (_upgradeCard != null)
            {
                _upgradeCardWindow.SetActive(true); 
                _titleText.text = "Purchase upgrade:"; 
                _cardName.text = _upgradeCard.CardName; 
                _cardIcon.sprite = _upgradeCard.CardIcon; 
                _cardDescription.text = _upgradeCard.CardDescription; 
                _cardCost.text = _upgradeCard.CardCost.ToString();
            }
            else
            {
                _upgradeCardWindow.SetActive(false); 
                _titleText.text = "No upgrade available";
            }
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(Upgrade);
        }

        private void Upgrade()
        {
            if (_upgradeCard == null) return;

            if (_playerData.Coins >= _upgradeCard.CardCost)
            {
                _playerData.UseCoins(_upgradeCard.CardCost);
                _playerUpgrade.AddUpgrade(_upgradeCard); 
                _upgradeButton.interactable = false; 
                _playerData.NearComputer.RemovePurchasedUpgrade(); 
                _hintManager.ShowAndHideHint($"You purchased upgrade: {_playerUpgrade.PurchasedUpgradeCards[^1].CardName}!"); 
                gameObject.SetActive(false);
            }
            else
            {
                _hintManager.ShowAndHideHint("Not enough coins");
            }

        }

        private void CloseUpgradeWindow()
        {
            gameObject.SetActive(false); 
            
        }
    }
}