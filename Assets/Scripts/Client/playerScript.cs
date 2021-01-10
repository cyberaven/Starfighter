using System;
using UnityEngine;

namespace Client
{
    //enum for filling field in unityEditor
    public enum MovementAdapter
    {
        PlayerControl,
        RemoteNetworkControl
    }
    
    public class PlayerScript : MonoBehaviour
    {
        public float shipSpeed, shipRotation;
        public MovementAdapter MovementAdapter;
        
        public IMovementAdapter ShipsBrain;
        
        private GameObject _front, _back, _left, _right;
        private Rigidbody _ship, _engine;
        private ParticleSystem _tlm, _trm, _blm, _brm, _te;
        private ConstantForce _thrustForce;
        
        [SerializeField]
        private Vector3 thrustForceVector;
        [SerializeField]
        private Vector3 maneurForceVector;
        

        private void Start()
        {
            // всякое говно при создании объекта
            _front = GameObject.Find("Front");
            _back = GameObject.Find("Back");
            _left = GameObject.Find("Left");
            _right = GameObject.Find("Right");
            _ship = GetComponent<Rigidbody>();
            _thrustForce = GetComponent<ConstantForce>();
            shipSpeed = 0;
            shipRotation = 0;
            
            _trm = GameObject.Find("TopRightEmition").GetComponent<ParticleSystem>();
            _tlm = GameObject.Find("TopLeftEmition").GetComponent<ParticleSystem>();
            _brm = GameObject.Find("BotRightEmition").GetComponent<ParticleSystem>();
            _blm = GameObject.Find("BotLeftEmition").GetComponent<ParticleSystem>();
            _te = GameObject.Find("ThurstsEmition").GetComponent<ParticleSystem>();

            switch (MovementAdapter)
            {
                case MovementAdapter.PlayerControl:
                    ShipsBrain = new PlayerControl();
                    break;
                case MovementAdapter.RemoteNetworkControl:
                    ShipsBrain = new RemoteNetworkControl();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            //for UI displaying
            shipSpeed = _ship.velocity.magnitude;
            shipRotation = _ship.angularVelocity.magnitude;
            
            UpdateMovement();
            AnimateMovement();
        }
        
        private void UpdateMovement()
        {
            // рассчет тяги
            thrustForceVector = _front.transform.position - _back.transform.position; //вектор фронтальной тяги
            maneurForceVector = _right.transform.position - _left.transform.position; //вектор боковой тяги
            _thrustForce.force = 
                (thrustForceVector / thrustForceVector.magnitude) * (ShipsBrain.GetThrustSpeed() + ShipsBrain.GetStraightManeurSpeed()) +
                (maneurForceVector / maneurForceVector.magnitude) * ShipsBrain.GetSideManeurSpeed();
            _thrustForce.torque = new Vector3(0, ShipsBrain.GetShipAngle(), 0);
        }
 
        private void AnimateMovement()
        {
            #region Reset movement animation
            
            _te.Stop();
            _tlm.Stop();
            _trm.Stop();
            _brm.Stop();
            _blm.Stop();
            
            #endregion
            
            var engines = ShipsBrain.getMovement();
            Debug.unityLogger.Log($"engines state: {engines.ToString()}");
            if (engines.Thrust)
            {
                _te.Play(true);
            }

            if (engines.TopRight)
            {
                _trm.Play(true);
            }

            if (engines.TopLeft)
            {
                _tlm.Play(true);
            }

            if (engines.BotLeft)
            {
                _blm.Play(true);
            }

            if (engines.BotRight)
            {
                _brm.Play(true);
            }
        }
    }
}