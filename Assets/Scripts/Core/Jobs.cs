using System;
using Client;
using Net.PackageData;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Utils;

namespace Core
{
    public struct MoveRotateJob : IJobParallelForTransform
    {
        [ReadOnly]
        public NativeList<Vector3> newPositions;
        [ReadOnly]
        public NativeList<Quaternion> newRotations;
        public float deltaTime;
        
        public void Execute(int index, TransformAccess transform)
        {
            transform.rotation = newRotations[index];
            transform.position = newPositions[index];
        }
    }
    
    // public struct StatePackageJob: IJobParallelFor
    // {
    //     [ReadOnly] 
    //     public NativeArray<WorldObject> WorldObjects;
    //     
    //     public void Execute(int index)
    //     {
    //         var wObj = WorldObjects[index];
    //
    //         var go = GameObject.Find(wObj.name.ToString());
    //
    //         if (go == null)
    //         {
    //             InstantiateHelper.InstantiateObject(wObj);
    //             return;
    //         }
    //         
    //         switch (wObj.unitType)
    //         {
    //             case UnitType.Ship:
    //                 var ps = go.GetComponent<PlayerScript>();
    //                 if (ps is null) break;
    //                 ps.shipSpeed = wObj.velocity;
    //                 ps.shipRotation = wObj.angularVelocity;
    //                 go.transform.position = wObj.position;
    //                 go.transform.rotation = wObj.rotation;
    //                 break;
    //             case UnitType.Asteroid:
    //             case UnitType.WayPoint:
    //             case UnitType.WorldObject:
    //             default:
    //                 go.transform.position = wObj.position;
    //                 go.transform.rotation = wObj.rotation;
    //                 break;
    //         }
    //     }
    // }
}