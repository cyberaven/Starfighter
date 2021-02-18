using System;
using System.Linq;
using Client.Utils;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using Net.Core;
using Net.Utils;
using UnityEngine;
using Object = UnityEngine.Object;
using Task = System.Threading.Tasks.Task;

namespace Net.PackageHandlers.ClientHandlers
{
    public class StatePackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            var statePack = pack as StatePackage;
            try
            {
                var dispatcherFlagDone = false;
                Dispatcher.Instance.Invoke(() =>
                {
                    foreach (var worldObject in statePack.data.worldState)
                    {
                        var gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                            .Union(GameObject.FindGameObjectsWithTag(Constants.PlayerTag))
                            .FirstOrDefault(go => go.name == worldObject.name);
                        
                        if (gameObject != null)
                        {
                            if(gameObject.CompareTag(Constants.PlayerTag)) continue;
                            gameObject.transform.position = worldObject.position;
                            gameObject.transform.rotation = worldObject.rotation;
                        }
                        else
                        {
                            InstantiateHelper.InstantiateObject(worldObject);
                        }
                    }
                    dispatcherFlagDone = true;
                });
                while(!dispatcherFlagDone){}
                dispatcherFlagDone = false;
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return;
            }
        }
    }
}