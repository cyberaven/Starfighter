using System;
using UnityEngine;
using EventType = Net.Utils.EventType;

namespace Net.PackageData
{
    [Serializable]
    public class EventData
    {
        //TODO: Event data. Types? Cases to use?
        [SerializeField]
        public Guid eventId;

        [SerializeField]
        public EventType eventType;
        
        [SerializeField]
        public object data;

        [SerializeField]
        public DateTime timeStamp;
    }
}
