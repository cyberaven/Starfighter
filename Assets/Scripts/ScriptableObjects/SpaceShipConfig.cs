using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpaceShipConfig", menuName = "Configs/SpaceShipConfig", order = 0)]
    [Serializable]
    public class SpaceShipConfig: SpaceUnitConfig
    {
        public float maxFuel;
    }
}