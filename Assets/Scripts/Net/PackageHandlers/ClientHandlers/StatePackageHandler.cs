using System;
using System.Linq;
using Client;
using Client.UI;
using Core;
using Core.ClassExtensions;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using UnityEngine;
using Utils;
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
                Dispatcher.Instance.Invoke(() =>
                {
                    foreach (var worldObject in statePack.data.worldState)
                    {
                        if (worldObject is Asteroid)
                        {
                            var go = InstantiateHelper.InstantiateObject(worldObject);
                            Debug.unityLogger.Log($"asteroid are added");
                            go.tag = Constants.AsteroidTag;
                            continue;
                        }

                        if (worldObject is WayPoint)
                        {
                            var gameObject = GameObject.FindGameObjectsWithTag(Constants.WayPointTag)
                                .FirstOrDefault(go => go.name == worldObject.name);
                        
                            if (gameObject != null)
                            {
                                //Сервер однозначно определяет положение ВСЕХ объектов
                                gameObject.transform.position = worldObject.position;
                                gameObject.transform.rotation = worldObject.rotation;
                            }
                            else
                            {
                                var go = InstantiateHelper.InstantiateObject(worldObject);
                                go.tag = Constants.WayPointTag;
                                var pointer = Resources.FindObjectsOfTypeAll<GPSView>().First();
                                pointer.SetTarget(go);
                                pointer.gameObject.SetActive(true);
                            }

                            continue;
                        }

                        if (worldObject is SpaceShip)
                        {
                            var gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                                .FirstOrDefault(go => go.name == worldObject.name);

                            if (gameObject != null)
                            {
                                //Сервер однозначно определяет положение ВСЕХ объектов
                                gameObject.transform.position = worldObject.position;
                                gameObject.transform.rotation = worldObject.rotation;
                                gameObject.GetComponent<PlayerScript>().shipRotation =
                                    (worldObject as SpaceShip).angularVelocity;
                                gameObject.GetComponent<PlayerScript>().shipSpeed =
                                    (worldObject as SpaceShip).velocity;
                                gameObject.GetComponent<PlayerScript>().shipConfig =
                                    (worldObject as SpaceShip).dto.ToConfig();
                            }
                            else
                            {
                                var go = InstantiateHelper.InstantiateObject(worldObject);
                                var ps = go.GetComponent<PlayerScript>();
                                if (ps is null) continue;
                                ps.shipConfig = (worldObject as SpaceShip).dto.ToConfig();
                                ps.shipConfig.shipState = (worldObject as SpaceShip).shipState;
                                Debug.unityLogger.Log($"STATE PACK: {ps.shipConfig.shipState}");
                                if (!MainClientLoop.instance.TryAttachPlayerControl(ps))
                                {
                                    ps.movementAdapter = MovementAdapter.RemoteNetworkControl;
                                }
                            }
                            continue;
                        }

                        //default: WorldObject
                        {
                            var gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                                .FirstOrDefault(go => go.name == worldObject.name);

                            if (gameObject != null)
                            {
                                //Сервер однозначно определяет положение ВСЕХ объектов
                                gameObject.transform.position = worldObject.position;
                                gameObject.transform.rotation = worldObject.rotation;
                            }
                            else
                            {
                                var go = InstantiateHelper.InstantiateObject(worldObject);
                                // var ps = go.GetComponent<PlayerScript>();
                                // if (ps is null) continue;
                                // if (!MainClientLoop.instance.TryAttachPlayerControl(ps))
                                // {
                                //     ps.movementAdapter = MovementAdapter.RemoteNetworkControl;
                                // }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return;
            }
        }
    }
}