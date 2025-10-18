using System;
using UnityEngine;

namespace Interactables
{
    public class EndGameTrigger : MonoBehaviour
    {
        public event Action OnEndGame;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnEndGame?.Invoke();
            }
        }
    }
}