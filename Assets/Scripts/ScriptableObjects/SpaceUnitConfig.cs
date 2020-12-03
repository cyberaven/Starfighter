using UnityEngine;
using System;
namespace Config
{
    [CreateAssetMenu(fileName = "SpaceUnitConfig", menuName = "Configs/SpaceUnitConfig", order = 0)]
    [Serializable]
    public class SpaceUnitConfig : ScriptableObject {
        public float maxAngleSpeed;
        public float maxSpeed;
        public float maxHP;
        public bool isDockable;
    }
}