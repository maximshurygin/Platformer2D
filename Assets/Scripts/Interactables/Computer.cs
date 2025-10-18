using System;
using GameManagers; using Player;
using UnityEngine;
using Upgrades;
using Zenject;
namespace Interactables 
{ public class Computer : MonoBehaviour 
{
    [SerializeField] private UpgradeCard _availableUpgradeCard; 
    private string _hintText = "Press E to interact"; 
    private HintManager _hintManager; 
    private PlayerData _playerData; 
    public event Action OnOutOfRange; 
    public UpgradeCard AvailableUpgradeCard => _availableUpgradeCard;

    [Inject]
    private void Construct(HintManager hintManager, PlayerData playerData)
    {
        _hintManager = hintManager; _playerData = playerData;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _hintManager.ShowHint(_hintText); 
            _playerData.SetIsComputerInRange(true); 
            _playerData.SetNearComputer(this); 
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        { 
            _hintManager.HideHint();
            _playerData.SetIsComputerInRange(false); 
            _playerData.SetNearComputer(null); 
            OnOutOfRange?.Invoke(); 
        }
    }

    public void RemovePurchasedUpgrade()
    {
        _availableUpgradeCard = null;
    }
} }