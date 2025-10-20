using UnityEngine;

namespace Interactables
{
    public class BouncePad : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _bounceForce = 15f;
        [SerializeField] private Animator _animator;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _animator.SetTrigger("Activated");
                _audioSource.Play();
                other.TryGetComponent(out Rigidbody2D rigidbody);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
                rigidbody?.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}