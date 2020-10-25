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

        // Use this for initialization
        void Start()
        {
            servManager = ServerManager.GetInstance();
            //Config init
            //Вероятно намудрил чего-то не того. Надо разобраться.
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
