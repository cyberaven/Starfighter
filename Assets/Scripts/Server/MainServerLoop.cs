using UnityEngine;
using System.Collections;
using Server.Core;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{

    public class MainServerLoop : ScriptableObject
    {
        public ServerManager servManager;
        public EventBus eventBus;
        // Use this for initialization
        void Start()
        {
            servManager = ServerManager.GetInstance();
            eventBus = EventBus.GetInstance();
            //Config init
            //TODO: Вероятно намудрил чего-то не того.
            var thread = new Thread(
                new ThreadStart(async () => { await servManager.WaitForConnectionAsync(new UdpSocket()); } ));
            thread.Start();
            
        }

        // Update is called once per frame
        void Update()
        {
            //SendBroadcast WorldState
        }
    }

}
