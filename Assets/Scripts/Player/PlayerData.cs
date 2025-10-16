using System;
using Interactables;
using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        public int Coins { get; private set; }
        public int Keys { get; private set; }
        public bool IsComputerInRange { get; private set; }
        
        public Computer NearComputer { get; private set; }

        public Transform LastCheckpoint { get; private set; }
        
        public event Action<int> OnCoinsChanged;
        public event Action<int> OnKeysChanged;

        public void AddCoin()
        {
            Coins++;
            OnCoinsChanged?.Invoke(Coins);
        }

        public void AddKey()
        {
            Keys++;
            OnKeysChanged?.Invoke(Keys);
        }

        public void UseCoins(int amount)
        {
            Coins -= amount;
            OnCoinsChanged?.Invoke(Coins);
        }

        public void UseKey()
        {
            Keys -= 1;
            Debug.Log($"Keys: {Keys}");
        }

        public void SetLastCheckpoint(Transform checkpoint)
        {
            LastCheckpoint = checkpoint;
        }

        public void SetIsComputerInRange(bool isNearPC)
        {
            IsComputerInRange = isNearPC;
        }

        public void SetNearComputer(Computer nearComputer)
        {
            NearComputer = nearComputer;
        }
    }
}