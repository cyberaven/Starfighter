using UnityEngine;

namespace Client
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