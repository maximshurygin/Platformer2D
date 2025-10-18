using System.Collections;
using UnityEngine;

namespace Interactables
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Vector2 _speedRange = new Vector2(1f, 2f);
        [SerializeField] private float _maxDisatnce = 1.5f;
        [SerializeField] private bool _invertDirection = false;
        private Vector3 _startPosition;
        private float _speed;

        private void OnEnable()
        {
            _startPosition = transform.position;
            _speed = Random.Range(_speedRange.x, _speedRange.y);
            StartCoroutine(MovePlatform());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.SetParent(null);
            }
        }


        private IEnumerator MovePlatform()
        {
            while (true)
            {
                float offset = Mathf.PingPong(Time.time * _speed, _maxDisatnce);
                if (_invertDirection)
                {
                    offset *= -1;
                }
                transform.position = _startPosition + new Vector3(offset, 0f, 0f);
                yield return null;
            }
        }
    }
}