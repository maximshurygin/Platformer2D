using System;
using Player;
using UnityEngine;
using Zenject;


namespace Loot
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] protected AudioSource _audioSource;
        
        protected PlayerData _playerData;
        
        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PickUp();
            }
        }

        protected virtual void PickUp()
        {
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position,1f);
            gameObject.SetActive(false);
        }
    }
}