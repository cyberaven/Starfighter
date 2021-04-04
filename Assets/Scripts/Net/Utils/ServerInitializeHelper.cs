using System;
using System.Collections;
using Core;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Net.Utils
{
    public class ServerInitializeHelper: Singleton<ServerInitializeHelper>
    {
        private SpaceShipConfig[] _shipConfigs;
        
        private new void Awake()
        {
            base.Awake();
        }

        public IEnumerator InitServer()
        {
            _shipConfigs = Resources.LoadAll<SpaceShipConfig>(Constants.PathToShips);
            foreach (var spaceShipConfig in _shipConfigs)
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
            foreach (var spaceShipConfig in _shipConfigs)
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