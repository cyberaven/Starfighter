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
                // var data = new NativeArray<WorldJobData>(statePack?.data.worldState.Select(x=>new WorldJobData(x)).ToArray(), Allocator.TempJob);
                // var job = new StatePackageJob()
                // {
                //     StateData = data
                // };
                // var handle = job.Schedule(data.Length, 4);
                // handle.Complete();
                // data.Dispose();
                MainClientLoop.instance.StatePackages.Push(statePack);
                // Dispatcher.Instance.Invoke(() =>
                // {
                //     
                //     
                // });
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return;
            }
        }
    }
}