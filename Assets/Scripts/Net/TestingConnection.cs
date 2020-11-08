using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Net.Core;
using Net.PackageData;
using Net.Packages;
using UnityEngine;

namespace Net
{
    public class TestingConnection : MonoBehaviour
    {
        private UdpSocket _socket;
        private void Awake()
        {
            _socket = new UdpSocket(new IPEndPoint(IPAddress.Loopback, 5000), 5001);
        }

        private void Start()
        {
        }

        private void Update()
        {
            Task.Run(() =>
            {
                _socket.SendPackageAsync(new ConnectPackage(new ConnectData()
                {
                    Login = "login", Password = "pass", AccountType = 0
                }));
            });

            Task.Run(() =>
            {
                var result = _socket.ReceivePackageAsync().Result;
                Debug.Log($"Received: {result}");
            });
        }
    }
}