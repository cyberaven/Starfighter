using System;
using Net;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TestConnector: MonoBehaviour
    {
        public Dropdown dropdown;
        public InputField input;
        public ClientAccountObject pilotAccountObject;
        public ClientAccountObject navigatorAccountObject;
        public string serverAddress;
        
        public void TryConnectToServer()
        {
            serverAddress = input.text;
            Debug.unityLogger.Log(dropdown.captionText.text);
            try
            {
                switch (dropdown.captionText.text)
                {
                    case "Pilot":
                        Debug.unityLogger.Log("Try connect as pilot");
                        MainClientLoop.instance.accountObject = pilotAccountObject;
                        ClientConnectionHelper.TryToConnect(serverAddress, pilotAccountObject.login,
                            pilotAccountObject.password, pilotAccountObject.type);
                        break;
                    case "Navigator":
                        Debug.unityLogger.Log("Try connect as navigator");
                        MainClientLoop.instance.accountObject = navigatorAccountObject;
                        ClientConnectionHelper.TryToConnect(serverAddress, navigatorAccountObject.login,
                            navigatorAccountObject.password, navigatorAccountObject.type);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }
    }
}