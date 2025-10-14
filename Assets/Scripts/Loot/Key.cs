using Player;
using UnityEngine;
using Zenject;

namespace Loot
{
    public class Key: Loot
    {
        protected override void PickUp()
        {
            base.PickUp();
            _playerData.AddKey();
        }
    }
}