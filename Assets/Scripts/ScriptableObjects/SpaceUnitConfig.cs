using UnityEngine;
using System;
namespace Config
{
    [CreateAssetMenu(fileName = "SpaceUnitConfig", menuName = "Configs/SpaceUnitConfig", order = 0)]
    [Serializable]
    public class SpaceUnitConfig : ScriptableObject {
        public float maxAngleSpeed;
        public float maxSpeed;
        public int maxHp;
        public int currentHp;
        public bool isDockable;
        public Vector3 position = Vector3.one;
        public Quaternion rotation = Quaternion.identity;
        public string prefabName;
    }
}