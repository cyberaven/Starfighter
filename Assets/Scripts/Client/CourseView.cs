using UnityEngine;

namespace Client
{
    public class CourseView : MonoBehaviour
    {
        public GameObject ship;
        private Rigidbody _shipRb;
        public Vector3 velocity;
        // Start is called before the first frame update
        void Awake()
        {
        }

        public void Init(PlayerScript playerScript)
        {
            ship = playerScript.gameObject;
            _shipRb = playerScript.GetComponent<Rigidbody>();
        }
    
        // Update is called once per frame
        void Update()
        {
            velocity = (_shipRb.velocity + transform.position);
            transform.position = ship.transform.position;
            transform.LookAt(velocity, Vector3.up);
        }
    }
}
