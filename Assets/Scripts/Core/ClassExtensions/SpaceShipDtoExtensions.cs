using Core.Models;
using ScriptableObjects;
using UnityEngine;

namespace Core.ClassExtensions
{
    public static class SpaceShipDtoExtensions
    {
        public static SpaceShipConfig ToConfig(this SpaceShipDto dto)
        {
            var shipConfig = ScriptableObject.CreateInstance<SpaceShipConfig>();
            shipConfig.currentStress = dto.currentStress;
            shipConfig.maxStress = dto.maxStress;
            shipConfig.shipId = dto.shipId;
            shipConfig.shipState = dto.shipState;
            shipConfig.position = dto.position;
            shipConfig.rotation = dto.rotation;
            shipConfig.currentHp = dto.currentHp;
            shipConfig.prefabName = dto.prefabName;
            shipConfig.maxAngleSpeed = dto.maxAngleSpeed;
            shipConfig.maxSpeed = dto.maxSpeed;
            shipConfig.maxHp = dto.maxHp;
            shipConfig.isDockable = dto.isDockable;
            shipConfig.shipState = dto.shipState;
            return shipConfig;
        }
    }
}