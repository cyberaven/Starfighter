using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Net.PackageData
{
    [Serializable]
    public class StateData
    {
        [SerializeField]
        public WorldObject[] worldState; //стоит ли так делать? Или какую-то структуру полегче? Это очень много инфы.
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


}
