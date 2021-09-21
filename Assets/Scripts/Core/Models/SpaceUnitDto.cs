using System;
using ScriptableObjects;
using UnityEngine;

namespace Core.Models
{
    [Serializable]
    public class SpaceUnitDto
    {
        public float maxAngleSpeed;
        public float maxSpeed;
        public float maxHp;
        public float currentHp;
        public bool isDockable;
        public bool isMovable;
        public Vector3 position = Vector3.one;
        public Quaternion rotation = Quaternion.identity;
        public string prefabName;
        public Guid id;
        
        public SpaceUnitDto(SpaceUnitConfig config)
        {
            maxAngleSpeed = config.maxAngleSpeed;
            maxSpeed = config.maxSpeed;
            maxHp = config.maxHp;
            currentHp = config.currentHp;
            isDockable = config.isDockable;
            isMovable = config.isMovable;
            position = config.position;
            rotation = config.rotation;
            prefabName = config.prefabName;
            id = config.id;
        }
    }
}