using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scripts
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Texture2D  defaultCursor;
        [SerializeField] private Texture2D  hoveringCursor;
        [SerializeField] private GameObject tutorialPanel;
        
        private void Start()
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void SwitchTutorialVisibility()
        {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void OnBeginHovering()
        {
            Cursor.SetCursor(hoveringCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        
        public void OnExitHovering()
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
