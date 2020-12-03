using System;
using UnityEngine;

namespace ScriptableObjects
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