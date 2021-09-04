using System;
using Core;
using Core.Models;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Net.PackageData
{
    [Serializable]
    public class StateData
    {
        [SerializeField]
        public WorldObject[] worldState;
    }

    [Serializable]
    public class WorldObject
    {
        public WorldObject(string _name, Transform _transform)
        {
            name = _name;
            position = _transform.position;
            rotation = _transform.rotation;
        }
        
        [SerializeField]
        public string name;
        [SerializeField]
        public Vector3 position;
        [SerializeField]
        public Quaternion rotation;
    }

    [Serializable]
    public class Asteroid : WorldObject
    {
        public Asteroid(string _name, Transform _transform) : base(_name, _transform)
        {}
    }
    
    [Serializable]
    public class WayPoint : WorldObject
    {
        public WayPoint(string _name, Transform _transform) : base(_name, _transform)
        {}
    }

    [Serializable]
    public class SpaceShip : WorldObject
    {
        [SerializeField]
        public Vector3 velocity;
        [SerializeField]
        public Vector3 angularVelocity;
        [SerializeField]
        public SpaceShipDto dto;
        [SerializeField]
        public UnitState shipState;

        public SpaceShip(string _name, Transform _transform, Vector3 _velocity, Vector3 _angularVelocity, SpaceShipDto _config, UnitState _shipState = UnitState.InFlight) : base(_name,
            _transform)
        {
            velocity = _velocity;
            angularVelocity = _angularVelocity;
            dto = _config;
            shipState = _shipState;
        }
    }

    
}
