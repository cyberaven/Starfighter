using System;
using System.Linq;
using Client;
using Client.Movement;
using Client.Utils;
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
        public async Task Handle(AbstractPackage pack)
        {
            var statePack = pack as StatePackage;
            try
            {
                Dispatcher.Instance.Invoke(() =>
                {
                    Debug.unityLogger.Log(statePack.data.worldState.ToList().Select(x => x.name).ToString());
                    foreach (var worldObject in statePack.data.worldState)
                    {
                        var gameObject = GameObject.FindGameObjectsWithTag(Constants.DynamicTag)
                            .FirstOrDefault(go => go.name == worldObject.name);

                        if (gameObject != null)
                        {
                            //не надо делать исключений для корабля. Сервер однозначно определяет положение ВСЕХ объектов
                            //if(gameObject.CompareTag(Constants.PlayerTag)) continue;
                            gameObject.transform.position = worldObject.position;
                            gameObject.transform.rotation = worldObject.rotation;
                        }
                        else
                        {
                            var go = InstantiateHelper.InstantiateObject(worldObject);
                            var ps = go.GetComponent<PlayerScript>();
                            if (ps is null) continue;
                            ps.movementAdapter = MovementAdapter.PlayerControl;
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