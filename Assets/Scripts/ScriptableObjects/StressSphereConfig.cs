using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "StressSphereConfig", menuName = "Configs/StressSphereConfig", order = 0)]
    [Serializable]
    public class StressSphereConfig : ScriptableObject
    {
        public float Damage;       
        public float Radius;        
    }
}