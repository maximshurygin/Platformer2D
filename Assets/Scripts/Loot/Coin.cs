using UnityEngine;

namespace Loot
{
    public class Coin : Loot
    {
        protected override void PickUp()
        {
            base.PickUp();
            _playerData.AddCoin();
        }
    }
}