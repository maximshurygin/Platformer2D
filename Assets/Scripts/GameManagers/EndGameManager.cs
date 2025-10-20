using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GameManagers
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _endGameWindow;
        [SerializeField] private EndGameTrigger _endGameTrigger;
        private PlayerController _playerController;
        private PlayerInput _playerInput;

        [Inject]
        private void Construct(PlayerController playerController, PlayerInput playerInput)
        {
            _playerController = playerController;
            _playerInput = playerInput;
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
            _playerController.ForceStop();
            _playerController.enabled = false;
            _playerInput.enabled = false;
        }
    }
}