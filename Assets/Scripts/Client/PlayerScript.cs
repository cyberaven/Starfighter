using System;
using Client.Movement;
using ScriptableObjects;
using UnityEngine;

namespace Client
{
    //enum for filling field in unityEditor
    public enum MovementAdapter
    {
        BlankControl,
        PlayerControl,
        RemoteNetworkControl
    }
    
    public class PlayerScript : MonoBehaviour
    {
        public float shipSpeed, shipRotation;
        public MovementAdapter movementAdapter;
        
        public IMovementAdapter ShipsBrain;

        [NonSerialized]
        public SpaceShipConfig shipConfig;
        
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
            
            _trm = gameObject.transform.Find("TopRightEmition").GetComponent<ParticleSystem>();
            _tlm = gameObject.transform.Find("TopLeftEmition").GetComponent<ParticleSystem>();
            _brm = gameObject.transform.Find("BotRightEmition").GetComponent<ParticleSystem>();
            _blm = gameObject.transform.Find("BotLeftEmition").GetComponent<ParticleSystem>();
            _te = gameObject.transform.Find("ThurstsEmition").GetComponent<ParticleSystem>();
            // _trm.gameObject.transform.parent = transform;
            // _tlm = GameObject.Find("TopLeftEmition").GetComponent<ParticleSystem>();
            // _tlm.gameObject.transform.parent = transform;
            // _brm = GameObject.Find("BotRightEmition").GetComponent<ParticleSystem>();
            // _brm.gameObject.transform.parent = transform;
            // _blm = GameObject.Find("BotLeftEmition").GetComponent<ParticleSystem>();
            // _blm.gameObject.transform.parent = transform;
            // _te = GameObject.Find("ThurstsEmition").GetComponent<ParticleSystem>();
            // _te.gameObject.transform.parent = transform;

            switch (movementAdapter)
            {
                case MovementAdapter.PlayerControl: //use on clients for ship under user control
                    ShipsBrain = new PlayerControl();
                    break;
                case MovementAdapter.RemoteNetworkControl: //use on server 
                    ShipsBrain = new RemoteNetworkControl();
                    break;
                case MovementAdapter.BlankControl: //use on client for other ships (?)
                    ShipsBrain = new BlankControl();
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
            // рассчет вектора тяги
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