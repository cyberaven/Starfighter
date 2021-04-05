using System;
using UnityEngine;

namespace Client
{
    public class CameraMotion : MonoBehaviour
    {
        private Camera _camera;
        public GameObject Player;
        private Vector3 _offset;
        private Vector3 _translationPoint;
        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
            _offset = Vector3.zero + Vector3.up*10;
        }
        
        void FreeMode()
        {
            _translationPoint = new Vector3(Input.GetAxis("Horizontal") * _camera.orthographicSize / 30,Input.GetAxis("Vertical") * _camera.orthographicSize / 30,0);
            _camera.transform.Translate(_translationPoint*-1);
        }
        
        void FollowShip()
        {
            transform.position = Player.transform.position + _offset;
        }
        
        private void LateUpdate()
        {
            if (FollowMode.active) FollowShip();
            else FreeMode();
        }
    }
}