using UnityEngine;

namespace Upgrades
{
    public enum UpgradeType
    {
        FireResist,
        ElectricResist,
    }
    
    [CreateAssetMenu(fileName = "UpgradeCard", menuName = "ScriptableObjects/Upgrade")]
    public class UpgradeCard : ScriptableObject
    {
        [Header("Card Info")]
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private string _cardName;
        [SerializeField] private Sprite _cardIcon;
        [SerializeField] private string _cardDescription;
        [SerializeField] private int _cardCost;
        [SerializeField] private int _id;

        public UpgradeType UpgradeType => _upgradeType;
        public string CardName => _cardName;
        public string CardDescription => _cardDescription;
        public Sprite CardIcon => _cardIcon;
        public int CardCost => _cardCost;
        public int ID => _id;
    }
}