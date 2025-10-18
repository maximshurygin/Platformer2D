using GameManagers;
using Scenes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        private PauseManager _pauseManager;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(PauseManager pauseManager, SceneLoader sceneLoader)
        {
            _pauseManager = pauseManager;
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(Resume);
            _exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(Resume);
            _exitButton.onClick.RemoveListener(Exit);
        }


        private void Resume()
        {
            _pauseManager.TogglePause();
        }

        private void Exit()
        {
            _sceneLoader.MainMenu();
        }
    }
}