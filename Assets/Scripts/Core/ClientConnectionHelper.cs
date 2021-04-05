using System;
using System.Linq;
using System.Net;
using Net;
using Net.Core;
using Net.PackageData;
using Net.PackageHandlers.ClientHandlers;
using Net.Packages;
using Net.Utils;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    
    public class ClientConnectionHelper: Singleton<ClientConnectionHelper>
    {
        private static AsyncOperation asyncPilot, asyncNavigator;
        private static StarfighterUdpClient _udpClient;
        
        public static void Init()
        {
        
        }

        public async void TryToConnect(string serverAddress, string login, string password, UserType accType)
        {
            //It's Client, so exchange ports
            _udpClient = new StarfighterUdpClient(IPAddress.Parse(serverAddress),
                         Constants.ServerReceivingPort,
                         Constants.ServerSendingPort);

            var connectData = new ConnectData()
            {
                login = login, password = password, accountType = accType
            };

            await _udpClient.SendPackageAsync(new ConnectPackage(connectData));
            Debug.unityLogger.Log($"connection package sent");
            var result = await _udpClient.ReceiveOnePackageAsync();
            _udpClient.Dispose();
            if (result is null)
            {
                result = new DeclinePackage(null);
            }
            Debug.unityLogger.Log($"connection response package received: {result.packageType}");
            switch (result.packageType)
            {
                case PackageType.ConnectPackage:
                {
                    Debug.unityLogger.Log("Server accept our connection");
                    Camera.main.gameObject.GetComponent<Camera>().enabled = false;
                    //TODO: Сервер принял подключение: загрузить сцену в зависимости от типа аккаунта или\и вызвать соотв событие.
                    switch (accType)
                    {
                        case UserType.Pilot:
                        {
                            SceneManager.LoadScene("pilot_UI");
                            break;
                        }
                        case UserType.Navigator:
                        {
                            SceneManager.LoadScene("navi_UI");
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException(
                                $"unexpected package type {result.packageType.ToString()}");
                    }
                    MainClientLoop.instance.Init(serverAddress);
                    NetEventStorage.GetInstance().connectToServer.Invoke(result as ConnectPackage);
                    
                    break;
                }
                case PackageType.DeclinePackage:
                    Debug.unityLogger.Log("Server decline our connection");
                    //TODO: Сервер отклонил подключение: вывести соотв сообщение и прочее
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"unexpected package type {result.packageType.ToString()}");
            }
        }
    }
}