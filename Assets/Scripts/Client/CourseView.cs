using Client.Movement;
using UnityEngine;

namespace Client
{
    public class CourseView : MonoBehaviour
    {
        public GameObject ship;
        private Vector3 _lastPosition;

        public void Init(PlayerScript playerScript)
        {
            ship = playerScript.gameObject;
        }
    
        // Update is called once per frame
        void Update()
        {
            var shipPosition = ship.transform.position;
            var delta = (shipPosition - _lastPosition).normalized;
            transform.LookAt(shipPosition + delta);
            transform.SetPositionAndRotation(shipPosition, transform.rotation);
            _lastPosition = shipPosition;
        }
    }
}
