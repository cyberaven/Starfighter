using UnityEngine;
using System.Collections.Generic;
using Server.Interfaces;

namespace Server.Core
{
    public class ServerManager
    {
        public static List<UpdSocket> sockets;

        private static ServerManager Instance;

        private ServerManager()
        {

        }

        public static ServerManager GetInstanceOfSErverManager()
        {
            if (Instance == null)
            {
                Instance = new ServerManager();
            }
            return Instance;
        }


        //private UpdSocket CreateNewListener()
        //{

        //}

        public void BroadcastSending(IPackage pack)
        {

        }
    }
}
