using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Server.Interfaces;

namespace Server.Core
{

    public class EventBus : ScriptableObject //?
    {
        private List<IPackageHandler> _packageHandlers;

        private static EventBus _instance;

        private EventBus()
        {
            _packageHandlers = new List<IPackageHandler>();
        }

        public static EventBus GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EventBus();
            }
            return _instance;
        }


        void Update()
        {

        }
    }
}
