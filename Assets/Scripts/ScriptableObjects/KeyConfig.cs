using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "KeyConfig", menuName = "Configs/KeyConfig", order = 0)]
    [Serializable]
    public class KeyConfig : ScriptableObject {
        // put your key bindings here
        // them have to bind with Unity axes for be able to use all sorts of controllers
        public KeyCode forward;
        public KeyCode backward;
        public KeyCode left;
        public KeyCode right;
        public KeyCode clockWiseRotate;
        public KeyCode counterclockWiseRotate;
        public KeyCode fire;
        public KeyCode dock;
        public KeyCode march;
        //...
    }
}
