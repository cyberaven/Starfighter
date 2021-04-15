using System;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.Packages;
using UnityEngine;

namespace Net.PackageHandlers.ServerHandlers
{
    public class ConnectPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            try
            {
                NetEventStorage.GetInstance().connectClient.Invoke(pack as ConnectPackage);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogError("Server connect pack handler",ex);
            }
        }
    }
}