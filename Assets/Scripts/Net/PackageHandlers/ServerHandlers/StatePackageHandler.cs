using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Net.Interfaces;
using Net.Packages;
using UnityEngine;
using Utils;

namespace Net.PackageHandlers.ServerHandlers
{
    public class StatePackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            var statePack = pack as StatePackage;
            try
            {
                Dispatcher.Instance.Invoke(() =>
                {
                    foreach (var worldObject in statePack.data.worldState)
                    {
                        var gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                            .FirstOrDefault(go => go.name == worldObject.name);

                        if (gameObject != null)
                        {
                            gameObject.transform.position = worldObject.position;
                            gameObject.transform.rotation = worldObject.rotation;
                        }
                        else
                        {
                            InstantiateHelper.InstantiateObject(worldObject);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return;
            }
        }
    }
}