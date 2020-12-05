using System;
using System.Security;
using UnityEngine;
using UnityEngine.Serialization;

namespace Net.PackageData
{
    [Serializable]
    public class EventData
    {
        //TODO: Event data. Types? Cases to use?
        [SerializeField]
        public Guid eventId;
        
        [SerializeField]
        public object data;

        [SerializeField]
        public DateTime timeStamp;
    }
}
