using UnityEngine;

namespace Client
{
    public class CourseView : MonoBehaviour
    {
        public GameObject ship;
        private GameObject _target;
        private Vector3 _lastPosition;

        public void Init(PlayerScript playerScript, GameObject target = null)
        {
            Debug.unityLogger.Log($"CourseView: {gameObject.name} , {playerScript.gameObject.name}");
            ship = playerScript.gameObject;
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
            var delta = (_target.transform.position - _lastPosition).normalized;
            transform.LookAt(shipPosition + delta);
            transform.SetPositionAndRotation(shipPosition, transform.rotation);
            _lastPosition = _target.transform.position;
        }
    }
}
