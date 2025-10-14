using Player;
using UnityEngine;

namespace Interactables
{
    public class Spikes : MonoBehaviour
    {
        private float _damage = 1000f;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(_damage);
            }
        }
    }
}