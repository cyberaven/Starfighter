using System;
using Core;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "RouteConfig", menuName = "Configs/RouteConfig", order = 0)]
    [Serializable]
    public class RouteConfig : ScriptableObject
    {
        public Vector2[] wayPoints;
        public InterpolationType interpolation;
    }
}

