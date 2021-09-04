using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Client;
using Client.Core;
using Client.UI;
using Core;
using Core.Models;
using Net.Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Net.Utils
{
    public class ServerInitializeHelper: Singleton<ServerInitializeHelper>
    {
        [Serializable]
        public class SpaceShipsWrapper
        {
            [SerializeField]
            public SpaceShipDto[] spaceShipConfigs;
        }

        private BinaryFormatter _binaryFormatter;
        private SpaceShipConfig[] _shipConfigs;
        
        private new void Awake()
        {
            base.Awake();
        }

        public IEnumerator InitServer()
        {
            try
            {
                Camera.main.gameObject.GetComponent<CameraMotion>().SwitchFollowMode();
                
                _shipConfigs = JsonUtility.FromJson<SpaceShipsWrapper>(File.ReadAllText(Constants.PathToShips))
                    .spaceShipConfigs.Select(x =>
                    {
                        var temp = ScriptableObject.CreateInstance<SpaceShipConfig>();
                        temp.maxStress = x.maxStress;
                        temp.currentStress = x.currentStress;
                        temp.shipId = x.shipId;
                        temp.maxAngleSpeed = x.maxAngleSpeed;
                        temp.maxSpeed = x.maxSpeed;
                        temp.maxHp = x.maxHp;
                        temp.currentHp = x.currentHp;
                        temp.isDockable = x.isDockable;
                        temp.position = x.position;
                        temp.rotation = x.rotation;
                        temp.prefabName = x.prefabName;
                        temp.shipState = x.shipState;
                        return temp;
                    }).ToArray();
            }
            catch (FileNotFoundException ex)
            {
                Debug.unityLogger.Log($"ERROR: {ex.Message}");
                _shipConfigs = Resources.LoadAll<SpaceShipConfig>(Constants.PathToShipsObjects);
            }
            catch (SerializationException ex)
            {
                Debug.unityLogger.Log($"ERROR {ex.Message}");
                _shipConfigs = Resources.LoadAll<SpaceShipConfig>(Constants.PathToShipsObjects);
            }
            
            foreach (var spaceShipConfig in _shipConfigs)
            {
                try
                {
                    var playerScript = InstantiateHelper.InstantiateServerShip(spaceShipConfig);
                    // playerScript.gameObject.GetComponentInChildren<DockingTrigger>()
                    //     .Init(playerScript);
                }
                catch(Exception ex)
                {
                    Debug.unityLogger.LogException(ex);
                }
                //TODO: Init fields from config;
                yield return null;
            }

            yield return StartCoroutine(
                Importer.AddAsteroidsOnScene(Importer.ImportAsteroids(Constants.PathToAsteroids)));
            MainServerLoop.instance.indicator.color = Color.green;
            NetEventStorage.GetInstance().worldInit.Invoke(0);
        }

        public void SaveServer()
        {
            foreach (var spaceShipConfig in _shipConfigs)
            {
                var ship = GameObject.Find(
                    $"{spaceShipConfig.prefabName}{Constants.Separator}{spaceShipConfig.shipId}");
                if (ship is null) continue;
                spaceShipConfig.rotation = ship.transform.rotation;
                spaceShipConfig.position = ship.transform.position;
                //Save other fields;
                spaceShipConfig.shipState = ship.GetComponent<PlayerScript>().GetState();
            }
            
            File.WriteAllText(Constants.PathToShips, JsonUtility.ToJson(new SpaceShipsWrapper()
            {
                spaceShipConfigs = _shipConfigs.Select(x=> new SpaceShipDto(x)).ToArray()
            }));
        }
    }
}