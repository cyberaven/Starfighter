using System;
using Client;
using Core;
using Net.PackageData;
using ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class InstantiateHelper
    {
        public static PlayerScript InstantiateServerShip(SpaceShipConfig ship)
        {
            var shipPrefab = Resources.Load(Constants.PathToPrefabs + ship.prefabName);
            var shipsInstance = Object.Instantiate(shipPrefab, position: ship.transform.position,
                rotation: ship.transform.rotation) as GameObject;
            shipsInstance.name = ship.prefabName + "_" + Guid.NewGuid();
            shipsInstance.tag = Constants.DynamicTag;
            
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
            instance.SetActive(true);
            return instance;
        }
    }
}