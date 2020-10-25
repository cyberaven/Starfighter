using System;
using System.Collections.Generic;
using UnityEngine;
using Server.Utils.Enums;


namespace Server.Interfaces
{
    public interface IPackage
    {
        PackageType PackageType { get; }
        object Data { get; } 
    }
}