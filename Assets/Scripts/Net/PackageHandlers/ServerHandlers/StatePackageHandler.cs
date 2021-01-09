using System;
using System.Linq;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using Net.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Net.PackageHandlers.ServerHandlers
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
                            .FirstOrDefault(go => go.name == worldObject.name);
                        
                        if (gameObject != null)
                        {
                            gameObject.transform.position = worldObject.position;
                            gameObject.transform.rotation = worldObject.rotation;
                        }
                        else
                        {
                            var prefabName = worldObject.name.Split('_')[0];
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