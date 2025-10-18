using Scenes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private Button _exitGameButton;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _exitGameButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            _sceneLoader.MainMenu();
        }
    }
}