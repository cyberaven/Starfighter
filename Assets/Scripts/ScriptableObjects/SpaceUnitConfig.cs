using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpaceUnitConfig", menuName = "Configs/SpaceUnitConfig", order = 0)]
    [Serializable]
    public class SpaceUnitConfig : ScriptableObject {
        public float maxAngleSpeed;
        public float maxSpeed;
        public int maxHp;
        public int currentHp;
        public bool isDockable;
        public Transform transform;
        public string prefabName;
    }
}