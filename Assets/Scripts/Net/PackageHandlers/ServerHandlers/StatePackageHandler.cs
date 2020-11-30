using System.Linq;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using UnityEngine;

namespace Net.PackageHandlers.ServerHandlers
{
    public class StatePackageHandler : IPackageHandler
    {
        public async Task Handle(IPackage pack)
        {
            foreach (var worldObject in ((pack as StatePackage).Data as StateData).worldState)
            {
                var gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                    .FirstOrDefault(go => go.name == worldObject.name);
                if (gameObject != null)
                {
                    Vector3.Lerp(gameObject.transform.position, worldObject.position, Time.deltaTime);
                    Quaternion.RotateTowards(gameObject.transform.rotation, worldObject.rotation, Time.deltaTime);
                }
                else
                {
                    var prefabName = worldObject.name.Split('_')[0];
                    Object goToInstantiate;
                    goToInstantiate = Resources.Load(Constants.PathToPrefabs + prefabName);
                    var instance = Object.Instantiate(goToInstantiate, worldObject.position, worldObject.rotation) as GameObject;
                    instance.name = worldObject.name;
                    instance.tag = Constants.DynamicTag;
                }
            }
        }
    }
}