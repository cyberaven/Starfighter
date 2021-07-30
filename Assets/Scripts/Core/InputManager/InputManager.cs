using System;
using System.Linq;
using Config;
using Net.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.InputManager
{
    public class InputManager: Singleton<InputManager>, IDisposable
    {
        public SmartAxis[] axes;
        public KeyConfig keyConfig;

        private void Awake()
        {
            axes = Resources.LoadAll<SmartAxis>(Constants.PathToAxes);
        }

        private void Update()
        {
            if (axes is null) return;
            foreach (var axis in axes)
            {
                axis.Update();
            }

            typeof(KeyConfig).GetFields().ToList().ForEach(x =>
            {
                if (Input.GetKeyDown(x.GetValue(keyConfig).ToString()))
                {
                    Enum.TryParse(x.GetValue(keyConfig).ToString(), out KeyCode value);
                    CoreEventStorage.GetInstance().actionKeyPressed.Invoke(value);
                }
            });
        }

        public void Dispose()
        {
            
        }
    }
}