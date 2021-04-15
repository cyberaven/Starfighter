using UnityEngine;

namespace Client
{
    public class Zoom : MonoBehaviour
    {
        public Camera navigatorCamera;

        private void LateUpdate()
        {
            navigatorCamera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 500;
            if(navigatorCamera.orthographicSize < 20) navigatorCamera.orthographicSize = 20;
            if(navigatorCamera.orthographicSize > 800) navigatorCamera.orthographicSize = 800;
        }
    }
}