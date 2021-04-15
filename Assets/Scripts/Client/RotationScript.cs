using UnityEngine;

namespace Client
{
    public class RotationScript : MonoBehaviour
    {
        public GameObject ship;
        private Rigidbody _shipRb;
        public Vector3 angVelocity;

        public int rotationSpeedModifier;
        // Start is called before the first frame update

        public void Init(PlayerScript playerScript)
        {
            ship = playerScript.gameObject;
            _shipRb = ship.GetComponent<Rigidbody>();
            rotationSpeedModifier = 5;
        }
    
        // Update is called once per frame
        void Update()
        {
            transform.position = ship.transform.position;
            angVelocity = (_shipRb.angularVelocity);
            transform.Rotate(angVelocity/rotationSpeedModifier);
        }
    }
}