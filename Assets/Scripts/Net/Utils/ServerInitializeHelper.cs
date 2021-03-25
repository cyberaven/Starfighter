using System;
using System.Collections;
using Core;
using ScriptableObjects;
using UnitBehavior;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Object = UnityEngine.Object;

namespace Net.Utils
{
    public class ServerInitializeHelper: Singleton<ServerInitializeHelper>
    {
        public SpaceShipConfig[] shipConfigs;
        
        private new void Awake()
        {
            base.Awake();
        }

        public IEnumerator InitServer()
        {
                foreach (var spaceShipConfig in shipConfigs)
                {
                    try
                    {
                        var playerScript = InstantiateHelper.InstantiateServerShip(spaceShipConfig);
                    }
                    catch(Exception ex)
                    {
                        Debug.unityLogger.LogException(ex);
                    }
                    //TODO: Init fields from config;
                    yield return null;
                }
        }

        public void SaveServer()
        {
            foreach (var spaceShipConfig in shipConfigs)
            {
                var ship = GameObject.Find($"{spaceShipConfig.prefabName}_{spaceShipConfig.shipId}");
                if(ship is null) continue;
                spaceShipConfig.rotation = ship.transform.rotation;
                spaceShipConfig.position = ship.transform.position;
                //TODO: Save other fields;
            }
        }
    }
}