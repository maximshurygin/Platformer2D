using System.Collections;
using UnityEngine;

namespace Effects
{
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField] private Material _flashMaterial;
        [SerializeField] private float _flashDuration = 0.2f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Material _originalMaterial;
        private Coroutine _flashCoroutine;
        private WaitForSeconds _flashDurationWait;

        private void Awake()
        {
            _flashDurationWait = new WaitForSeconds(_flashDuration);
            _originalMaterial = _spriteRenderer.material;
        }

        private void OnDisable()
        {
            _flashCoroutine = null;
            _spriteRenderer.material = _originalMaterial;
        }

        public void Flash()
        {
            if (_flashCoroutine != null)
            {
                StopCoroutine(_flashCoroutine);
            }
            _flashCoroutine = StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            _spriteRenderer.material = _flashMaterial;
            yield return _flashDurationWait;
            _spriteRenderer.material = _originalMaterial;
            _flashCoroutine = null;
        }
    }
}