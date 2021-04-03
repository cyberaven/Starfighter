using System;
using UnityEngine;

namespace Core.InputManager
{
    public class InputManager: Singleton<InputManager>, IDisposable
    {
        public SmartAxis[] axes;

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
        }

        public void Dispose()
        {
            
        }
    }
}