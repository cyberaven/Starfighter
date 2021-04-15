using UnityEngine;

namespace Client
{
    public class CourseView : MonoBehaviour
    {
        public GameObject ship;
        private PlayerScript _playerScript;
        private GameObject _target;
        private Vector3 _lastPosition;
        private Quaternion _lastRotation;

        public void Init(PlayerScript playerScript, GameObject target = null)
        {
            Debug.unityLogger.Log($"CourseView: {gameObject.name} , {playerScript.gameObject.name}");
            ship = playerScript.gameObject;
            _playerScript = playerScript;
            if (target is null)
                _target = ship;
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
        
        // Update is called once per frame
        void Update()
        {
            var shipPosition = ship.transform.position;
            var delta = (_target.transform.position - _lastPosition);
            transform.LookAt(shipPosition + delta.normalized);
            transform.SetPositionAndRotation(shipPosition, transform.rotation);
            
            _playerScript.shipSpeed = delta.magnitude / Time.deltaTime;
            _playerScript.shipRotation =
                (transform.rotation.eulerAngles - _lastRotation.eulerAngles).magnitude / Time.deltaTime;
            
            _lastPosition = _target.transform.position;
            _lastRotation = transform.rotation;
        }
    }
}
