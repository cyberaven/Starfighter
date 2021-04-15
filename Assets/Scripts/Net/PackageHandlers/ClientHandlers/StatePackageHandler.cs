using System;
using System.Linq;
using Client;
using Core;
using Net.Interfaces;
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
                    if (statePack.data.worldState.Any(x => x.name.Contains("CoursePoint")))
                    {
                        Debug.unityLogger.Log("WayPoint spawning");
                        foreach (var waypoint in statePack.data.worldState)
                        {
                            var gameObject = GameObject.FindGameObjectsWithTag(Constants.WayPointTag)
                                .FirstOrDefault(go => go.name == waypoint.name);
                        
                            if (gameObject != null)
                            {
                                //Сервер однозначно определяет положение ВСЕХ объектов
                                gameObject.transform.position = waypoint.position;
                                gameObject.transform.rotation = waypoint.rotation;
                            }
                            else
                            {
                                var go = InstantiateHelper.InstantiateObject(waypoint);
                                go.tag = Constants.WayPointTag;
                                var pointer = Resources.FindObjectsOfTypeAll<GPSView>().FirstOrDefault();
                                pointer.SetTarget(go);
                                pointer.gameObject.SetActive(true);
                            }
                        }
                        return;
                    }
                    
                    if (statePack.data.worldState.Any(x => x.name.Contains("Asteroid")))
                    {
                        Debug.unityLogger.Log("Asteroid spawning");
                        foreach (var asteroid in statePack.data.worldState)
                        {
                            var go = InstantiateHelper.InstantiateObject(asteroid);
                            Debug.unityLogger.Log($"asteroid are added");
                            go.tag = Constants.AsteroidTag;
                        }
                        return;
                    }
                    
                    foreach (var worldObject in statePack.data.worldState)
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
                            var ps = go.GetComponent<PlayerScript>();
                            if (ps is null) continue;
                            if (!MainClientLoop.instance.TryAttachPlayerControl(ps))
                            {
                                ps.movementAdapter = MovementAdapter.RemoteNetworkControl;
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