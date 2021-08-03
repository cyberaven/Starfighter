using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpaceUnitConfig", menuName = "Configs/SpaceUnitConfig", order = 0)]
    [Serializable]
    public class SpaceUnitConfig : ScriptableObject {
        public float maxAngleSpeed;
        public float maxSpeed;
        public float maxHp;
        public float currentHp;
        public bool isDockable;
        public Vector3 position = Vector3.one;
        public Quaternion rotation = Quaternion.identity;
        public string prefabName;
    }
}