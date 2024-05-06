using Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scripts
{
    public class GameMenuManager : SimpleSingleton<GameMenuManager>
    {
        [SerializeField] private GameObject pausePanel;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SwitchVisibility();
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }
        
        public void SwitchVisibility()
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            Time.timeScale = pausePanel.activeSelf ? 0f : 1f;
        }
        
        public void Exit()
        {
            Application.Quit();
        }
    }
}
