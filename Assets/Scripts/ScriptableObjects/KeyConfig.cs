using UnityEngine;
using System;

namespace Config
{
    [CreateAssetMenu(fileName = "KeyConfig", menuName = "Configs/KeyConfig", order = 0)]
    [Serializable]
    public class KeyConfig : ScriptableObject {
        // put your key bindings here
        // them have to bind with Unity axes for be able to use all sorts of controllers
        public KeyCode fire;
        public KeyCode dock;
    }
}
