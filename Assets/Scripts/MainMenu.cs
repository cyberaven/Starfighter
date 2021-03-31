using Core;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Toggle pilotToggle;
    public Toggle naviToggle;
    public UserType accType;
    public InputField loginField;
    public InputField passwordField;
    public InputField serverField;
    public void PlayGame()
    {
        if (pilotToggle.isOn)
        {
            accType = UserType.Pilot;
        }
        if (naviToggle.isOn)
        {
            accType = UserType.Navigator;
        }
        //TODO: допилить меню "выберите роль"
        ClientConnectionHelper.TryToConnect(serverAddress:serverField.text, login:loginField.text, password:passwordField.text, accType:accType);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}