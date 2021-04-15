using UnityEngine;

namespace Client
{
    public class Zoom : MonoBehaviour
    {
        public Camera navigatorCamera;

        private void LateUpdate()
        {
            navigatorCamera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 1000;
            if(navigatorCamera.orthographicSize < 20) navigatorCamera.orthographicSize = 20;
            if(navigatorCamera.orthographicSize > 3000) navigatorCamera.orthographicSize = 3000;
        }
    }
}