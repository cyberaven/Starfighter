using System;
using System.Linq;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using Net.Core;
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
                foreach (var worldObject in ((pack as StatePackage).Data as StateData).worldState)
                {
                    var gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                        .FirstOrDefault(go => go.name == worldObject.name);
                    Debug.unityLogger.Log($"StateClientHandle:{gameObject?.name}");
                    if (gameObject != null)
                    {
                        Vector3.Lerp(gameObject.transform.position, worldObject.position, Time.deltaTime);
                        Quaternion.RotateTowards(gameObject.transform.rotation, worldObject.rotation, Time.deltaTime);
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
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return;
            }
        }
    }
}