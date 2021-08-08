using System;
using ScriptableObjects;
using UnityEngine;

namespace Core.Models
{
    [Serializable]
    public class SpaceShipDto
    {
        public float maxStress;
        public float currentStress;
        public string shipId;
        public float maxAngleSpeed;
        public float maxSpeed;
        public float maxHp;
        public float currentHp;
        public bool isDockable;
        public Vector3 position = Vector3.one;
        public Quaternion rotation = Quaternion.identity;
        public string prefabName;
        public UnitState shipState;

        public SpaceShipDto(SpaceShipConfig config)
        {
            maxStress = config.maxStress;
            currentStress = config.currentStress;
            shipId = config.shipId;
            maxAngleSpeed = config.maxAngleSpeed;
            maxSpeed = config.maxSpeed;
            maxHp = config.maxHp;
            currentHp = config.currentHp;
            isDockable = config.isDockable;
            position = config.position;
            rotation = config.rotation;
            prefabName = config.prefabName;
        }
    }
}