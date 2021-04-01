using Core;
using UnityEngine;

namespace Client.Core
{
    public class ClientInitManager: Singleton<ClientInitManager>
    {
        protected new void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            ClientEventStorage.GetInstance().InitNavigator.AddListener(InitNavigator);
            ClientEventStorage.GetInstance().InitPilot.AddListener(InitPilot);
        }

        public void InitPilot(PlayerScript ps)
        {
            //TODO: normal pilot init
            ps.movementAdapter = MovementAdapter.PlayerControl;
            ps.gameObject.GetComponent<Collider>().enabled = false;
            var followComp = Camera.main.gameObject.GetComponent<Follow>();
            Camera.main.orthographicSize = 25;
            followComp.Player = ps.gameObject;
            followComp.enabled = true;
        }

        public void InitNavigator(PlayerScript ps)
        {
            //TODO: normal nav init
            ps.movementAdapter = MovementAdapter.BlankControl;
            ps.gameObject.GetComponent<Collider>().enabled = false;
            ps.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            ps.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            var followComp = Camera.main.gameObject.GetComponent<Follow>();
            Camera.main.orthographicSize = 50;
            followComp.Player = ps.gameObject;
            followComp.enabled = true;
            Camera.main.gameObject.AddComponent<Zoom>();
        }
    }
}