using UnityEngine;

namespace GameManagers
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        private bool _isPaused;

        public void TogglePause()
        {
            if (_isPaused)
            {
                SetPause(false);
                _pauseMenu.SetActive(false);
            }
            else
            {
                SetPause(true);
                _pauseMenu.SetActive(true);
            }
        }
        
        private void SetPause(bool paused)
        {
            Time.timeScale = paused ? 0 : 1;
            _isPaused = paused;
        }
    }
}