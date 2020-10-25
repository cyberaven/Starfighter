using UnityEngine;
using System.Collections.Generic;
using System;


namespace Server.PackageData
{
    [Serializable]
    public class StateData
    {
        public List<GameObject> worldState; //стоит ли так делать? Или какую-то структуру по-легче? Это очень много инфы.
        //TODO: World state data;
    }
}
