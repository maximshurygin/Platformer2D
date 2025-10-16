using System;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    public class PlayerUpgrade : MonoBehaviour
    {
        private List<UpgradeCard> _purchasedUpgradeCards = new List<UpgradeCard>();
        public List<UpgradeCard> PurchasedUpgradeCards => _purchasedUpgradeCards;
        
        public event Action<UpgradeCard> OnUpgradeAdded;
        
        public void AddUpgrade(UpgradeCard upgrade)
        {
            if (_purchasedUpgradeCards.Contains(upgrade)) return;
            _purchasedUpgradeCards.Add(upgrade);
            OnUpgradeAdded?.Invoke(upgrade);
        }
    }
}