using UnityEngine;

namespace Client.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI;

        public void Resume() 
        { 
            pauseMenuUI.SetActive(false);
        }

        public void Quit() 
        {
            Application.Quit();
        }
    }
}