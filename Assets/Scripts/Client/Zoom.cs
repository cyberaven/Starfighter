using UnityEngine;

namespace Client
{
    public class Zoom : MonoBehaviour
    {
        public Camera navigatorCamera;

        private void LateUpdate()
        {
            navigatorCamera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 50;
            if(navigatorCamera.orthographicSize < 20) navigatorCamera.orthographicSize = 20;
            if(navigatorCamera.orthographicSize > 200) navigatorCamera.orthographicSize = 200;
        }
    }
}