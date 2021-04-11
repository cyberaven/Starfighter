using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{


    public class MainMenu : MonoBehaviour
    {
        public UnityEngine.UI.Toggle pilotToggle;
        public UnityEngine.UI.Toggle naviToggle;
        public UserType accType;
        public InputField loginField;
        public InputField passwordField;
        public InputField serverField;
        public string SceneName;

        private void Start()
        {
            Application.targetFrameRate = 160;
        }

        public void PlayGame()
        {
            if (pilotToggle.isOn)
            {
                accType = UserType.Pilot;
                SceneName = "pilot_UI";
            }

            if (naviToggle.isOn)
            {
                accType = UserType.Navigator;
                SceneName = "navi_UI";
            }

            //TODO: допилить меню "выберите роль"
            ClientConnectionHelper.instance.TryToConnect(serverAddress: serverField.text, login: loginField.text,
                password: passwordField.text, accType: accType);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}