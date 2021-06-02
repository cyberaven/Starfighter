using UnityEngine;
using System;

namespace Config
{
    [CreateAssetMenu(fileName = "SpaceShipConfig", menuName = "Configs/SpaceShipConfig", order = 0)]
    [Serializable]
    public class SpaceShipConfig: SpaceUnitConfig
    {
        public float maxStress;
        public float currentStress;
        public string shipId;
    }
}