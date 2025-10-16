using System.Collections;
using GameManagers;
using UnityEngine;
using Zenject;


namespace Loot
{
    public class Key: Loot
    {
        [SerializeField] private string _hintText = "Found a key!";
        private HintManager _hintManager;

        [Inject]
        private void Construct(HintManager hintManager)
        {
            _hintManager = hintManager;
        }
        
        protected override void PickUp()
        {
            base.PickUp();
            _playerData.AddKey();
            _hintManager.ShowAndHideHint(_hintText);
        }
        
    }
}