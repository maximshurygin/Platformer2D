using Interactables;
using UnityEngine;
using Zenject;

namespace GameManagers
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _endGameWindow;
        [SerializeField] private EndGameTrigger _endGameTrigger;
        private PlayerController _playerController;

        [Inject]
        private void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void OnEnable()
        {
            _endGameTrigger.OnEndGame += EndGame;
        }
        private void OnDisable()
        {
            _endGameTrigger.OnEndGame -= EndGame;
        }

        private void EndGame()
        {
            _endGameWindow.SetActive(true);
            _playerController.enabled = false;
        }
    }
}