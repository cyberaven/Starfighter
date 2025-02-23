﻿using System;
using System.Linq;
using Net.Core;
using ScriptableObjects;
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
            keyConfig = Resources.Load<KeyConfig>(Constants.PathToKeys + "UserKeys");
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
                Enum.TryParse(x.GetValue(keyConfig).ToString(), out KeyCode value);
                if (!Input.GetKeyDown(value)) return;
                CoreEventStorage.GetInstance().actionKeyPressed.Invoke(value);
            });
        }

        public void Dispose()
        {
            
        }
    }
}