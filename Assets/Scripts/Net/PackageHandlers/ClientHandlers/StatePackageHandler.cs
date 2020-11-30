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
        public async Task Handle(IPackage pack)
        {
            try
            {
                var dispatcherFlagDone = false;
                Dispatcher.Instance.Invoke(() =>
                {
                    Debug.unityLogger.Log($"StateClientHandle:{(pack.Data as StateData).worldState.Length}");
                    foreach (var worldObject in (pack.Data as StateData).worldState)
                    {
                        GameObject gameObject = null;
                        Debug.unityLogger.Log($"StateClientHandle:{worldObject.name}");

                        gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                            .FirstOrDefault(go => go.name == worldObject.name);

                        Debug.unityLogger.Log($"StateClientHandle:{gameObject?.name}");
                        if (gameObject != null)
                        {
                            gameObject.transform.position = worldObject.position;
                            gameObject.transform.rotation = worldObject.rotation;
                            // Vector3.Lerp(gameObject.transform.position, worldObject.position, Time.deltaTime);
                            // Quaternion.RotateTowards(gameObject.transform.rotation, worldObject.rotation, Time.deltaTime);
                        }
                        else
                        {
                            var prefabName = worldObject.name.Split('_')[0];
                            Object goToInstantiate = null;
                            Debug.unityLogger.Log($"Try to load resource: {Constants.PathToPrefabs + prefabName}");
                            goToInstantiate = Resources.Load(Constants.PathToPrefabs + prefabName);
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