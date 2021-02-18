using System;
using Net.Core;
using Net.PackageData;
using ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Utils
{
    public static class InstantiateHelper
    {
        public static PlayerScript InstantiateShip(ClientAccountObject account)
        {
            var shipPrefab = Resources.Load(Constants.PathToPrefabs + account.ship.name);
            var shipsInstance = Object.Instantiate(shipPrefab, position: account.ship.transform.position,
                rotation: account.ship.transform.rotation) as GameObject;
            shipsInstance.name += "_" + Guid.NewGuid();
            
            var playerScript = shipsInstance.GetComponent<PlayerScript>();
            if(playerScript is null) shipsInstance.AddComponent<PlayerScript>();
            playerScript.movementAdapter = MovementAdapter.RemoteNetworkControl;
            
            shipsInstance.SetActive(true);
            
            return playerScript;
        }

        public static GameObject InstantiateObject(WorldObject worldObject)
        {
            var prefabName = worldObject.name.Split('_')[0];
            Debug.unityLogger.Log($"Try to load resource: {Constants.PathToPrefabs + prefabName}");
            var goToInstantiate = Resources.Load(Constants.PathToPrefabs + prefabName);
            var instance =
                Object.Instantiate(goToInstantiate, worldObject.position, worldObject.rotation) as
                    GameObject;
            instance.name = worldObject.name;
            instance.tag = Constants.DynamicTag;
            return instance;
        }
    }
}