using System;
using System.Net;
using Net.Core;
using Net.PackageData;
using Net.Packages;
using Net.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    
    public static class ClientConnectionHelper
    {
        private static AsyncOperation asyncPilot, asyncNavigator;
        private static StarfighterUdpClient _udpClient;
        public static void Init()
        {
        
        }

        public static async void TryToConnect(string serverAddress, string login, string password, UserType accType)
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
            Debug.unityLogger.Log($"connection response package received: {result.packageType}");
            switch (result.packageType)
            {
                case PackageType.ConnectPackage:
                {
                    Debug.unityLogger.Log("Server accept our connection");
                    //TODO: Сервер принял подключение: загрузить сцену в зависимости от типа аккаунта или\и вызвать соотв событие.
                    switch (accType)
                    {
                        case UserType.Pilot:
                        {
                            SceneManager.LoadSceneAsync("sth").allowSceneActivation = false;

                            if (SceneManager.LoadSceneAsync("sth").isDone)
                            {
                                NetEventStorage.GetInstance().connectToServer.Invoke(result as ConnectPackage);
                                SceneManager.LoadSceneAsync("sth").allowSceneActivation = true;
                            }

                            break;
                        }
                        case UserType.Navigator:
                        {
                            SceneManager.LoadSceneAsync("navi_UI").allowSceneActivation = false;
                            
                            if (SceneManager.LoadSceneAsync("navi_UI").isDone)
                            {
                                NetEventStorage.GetInstance().connectToServer.Invoke(result as ConnectPackage);
                                SceneManager.LoadSceneAsync("navi_UI").allowSceneActivation = true;
                            }

                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException(
                                $"unexpected package type {result.packageType.ToString()}");
                    }
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