using UnityEngine;

namespace Client.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject PauseMenuUI;

        public void Resume() 
        {
            PauseMenuUI.SetActive(false);
        }

        public void Quit() 
        {
            Application.Quit();
        }
    }
}