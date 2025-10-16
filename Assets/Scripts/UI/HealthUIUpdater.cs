using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class HealthUIUpdater : MonoBehaviour
    {
        [SerializeField] private Image playerHealthImage;
        private PlayerHealth _playerHealth;
        
        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void OnEnable()
        {
            _playerHealth.OnHealthChanged += UpdateHealthBar;
            playerHealthImage.fillAmount = 1f;
        }
        
        private void OnDisable()
        {
            _playerHealth.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            playerHealthImage.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
            playerHealthImage.fillAmount = Mathf.Clamp01(playerHealthImage.fillAmount);
        }
    }
}