using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        public int Coins { get; private set; }
        public int Keys { get; private set; }

        public Transform LastCheckpoint { get; private set; }

        public void AddCoin()
        {
            Coins++;
            Debug.Log($"Coins: {Coins}");
        }

        public void AddKey()
        {
            Keys++;
            Debug.Log($"Keys: {Keys}");
        }

        public void UseCoins(int amount)
        {
            Coins -= amount;
            Debug.Log($"Coins: {Coins}");

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
    }
}