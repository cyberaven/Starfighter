using Core.Models;
using ScriptableObjects;
using UnityEngine;

namespace Core.ClassExtensions
{
    public static class DtoExtensions
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
        
        public static SpaceUnitConfig ToConfig(this SpaceUnitDto dto)
        {
            var unitConfig = ScriptableObject.CreateInstance<SpaceUnitConfig>();

            unitConfig.position = dto.position;
            unitConfig.rotation = dto.rotation;
            unitConfig.currentHp = dto.currentHp;
            unitConfig.prefabName = dto.prefabName;
            unitConfig.maxAngleSpeed = dto.maxAngleSpeed;
            unitConfig.maxSpeed = dto.maxSpeed;
            unitConfig.maxHp = dto.maxHp;
            unitConfig.isDockable = dto.isDockable;
            return unitConfig;
        }
    }
}