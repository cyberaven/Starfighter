using UnityEngine;
using System;

namespace Config
{
    [CreateAssetMenu(fileName = "DangerZoneConfig", menuName = "Configs/DangerZoneConfig", order = 0)]
    [Serializable]
    public class DangerZoneConfig : ScriptableObject {
        public float Damage;
        public Vector3 Center;
        public float Radius;
        public Color Color;
    }
}
