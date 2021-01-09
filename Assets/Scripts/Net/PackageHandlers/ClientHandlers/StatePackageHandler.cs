using System;
using System.Linq;
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
                            var prefabName = worldObject.name.Split('_')[0];
                            Debug.unityLogger.Log($"Try to load resource: {Constants.PathToPrefabs + prefabName}");
                            var goToInstantiate = Resources.Load(Constants.PathToPrefabs + prefabName);
                            var instance =
                                Object.Instantiate(goToInstantiate, worldObject.position, worldObject.rotation) as
                                    GameObject;
                            instance.name = worldObject.name;
                            instance.tag = Constants.DynamicTag;
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