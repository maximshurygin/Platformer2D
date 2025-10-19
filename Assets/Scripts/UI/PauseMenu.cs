using GameManagers;
using Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _musicButton;
        [SerializeField] private TextMeshProUGUI _musicText;
        [SerializeField] private AudioSource _audioSource;

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
            _musicButton.onClick.AddListener(ToggleMusic);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(Resume);
            _exitButton.onClick.RemoveListener(Exit);
            _musicButton.onClick.RemoveListener(ToggleMusic);
        }


        private void Resume()
        {
            _pauseManager.TogglePause();
        }

        private void Exit()
        {
            _sceneLoader.MainMenu();
        }

        private void ToggleMusic()
        {
            if (_audioSource.mute)
            {
                _audioSource.mute = false;
                _musicText.text = "Music:on";
            }
            else
            {
                _audioSource.mute = true;
                _musicText.text = "Music:off";
            }
        }
    }
}