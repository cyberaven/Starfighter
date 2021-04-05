using Core;
using UnityEngine;

namespace Client.Core
{
    public class ClientInitManager: Singleton<ClientInitManager>
    {
        protected new void Awake()
        {
            base.Awake();
            
            ClientEventStorage.GetInstance().InitNavigator.AddListener(InitNavigator);
            ClientEventStorage.GetInstance().InitPilot.AddListener(InitPilot);
        }


        private static void InitPilot(PlayerScript ps)
        {
            //TODO: normal pilot init
            ps.movementAdapter = MovementAdapter.PlayerControl;
            ps.gameObject.GetComponent<Collider>().enabled = false;
            ps.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            ps.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            var cam = FindObjectOfType<Camera>();
            var followComp = cam.gameObject.GetComponent<Follow>() ?? cam.gameObject.AddComponent<Follow>();
            cam.orthographicSize = 25;
            followComp.Player = ps.gameObject;
            followComp.enabled = true;
            FindObjectOfType<DataOutput>().Init(ps);
            FindObjectOfType<CourseView>().Init(ps);
            FindObjectOfType<CoordinatesUI>().Init(ps);
        }

        private static void InitNavigator(PlayerScript ps)
        {
            //TODO: normal nav init
            ps.movementAdapter = MovementAdapter.RemoteNetworkControl;
            ps.gameObject.GetComponent<Collider>().enabled = false;
            ps.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            ps.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            var cam = FindObjectOfType<Camera>();
            var followComp = cam.gameObject.GetComponent<Follow>()??cam.gameObject.AddComponent<Follow>();
            cam.orthographicSize = 50;
            followComp.Player = ps.gameObject;
            followComp.enabled = true;
            var zoomComp = cam.gameObject.GetComponent<Zoom>()??cam.gameObject.AddComponent<Zoom>();
            zoomComp.navigatorCamera = cam;
            zoomComp.enabled = true;
        }
    }
}