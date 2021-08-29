using System;
using Client.Core;
using Client.Movement;
using Core;
using Net.Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

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
        public Vector3 shipSpeed, shipRotation;
        public MovementAdapter movementAdapter;
        public UnitStateMachine unitStateMachine;
        public IMovementAdapter ShipsBrain;
        [NonSerialized]
        public SpaceShipConfig shipConfig;
        public PlayerScript lastThingToDock;
        
        public bool readyToDock = false;
        
        private Transform _front, _back, _left, _right;
        private Rigidbody _ship, _engine;
        private ParticleSystem _tlm, _trm, _blm, _brm, _te;
        private ConstantForce _thrustForce;

        private void Start()
        {
            Debug.unityLogger.Log(shipConfig);
            //TODO: Init state from shipConfig not const
            unitStateMachine = new UnitStateMachine(gameObject, UnitState.InFlight);
            
            _front = gameObject.transform.Find("Front");
            _back = gameObject.transform.Find("Back");
            _left = gameObject.transform.Find("Left");
            _right = gameObject.transform.Find("Right");
            _ship = GetComponent<Rigidbody>();
            _thrustForce = GetComponent<ConstantForce>();
            shipSpeed = Vector3.zero;
            shipRotation = Vector3.zero;
            
            _trm = gameObject.transform.Find("TopRightEmition").GetComponent<ParticleSystem>();
            _tlm = gameObject.transform.Find("TopLeftEmition").GetComponent<ParticleSystem>();
            _brm = gameObject.transform.Find("BotRightEmition").GetComponent<ParticleSystem>();
            _blm = gameObject.transform.Find("BotLeftEmition").GetComponent<ParticleSystem>();
            _te = gameObject.transform.Find("ThurstsEmition").GetComponent<ParticleSystem>();

            switch (movementAdapter)
            {
                case MovementAdapter.PlayerControl: //use on clients for ship under user control
                    ShipsBrain = new PlayerControl(GetState());
                    break;
                case MovementAdapter.RemoteNetworkControl: //use on server 
                    ShipsBrain = new RemoteNetworkControl();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public UnitState GetState()
        {
            return unitStateMachine.currentState.State;
        }
        
        private void FixedUpdate()
        {
            UpdateMovement();
            AnimateMovement();
        }
        
        private void UpdateMovement()
        {
            // расчет вектора тяги
            var thrustForceVector = _front.transform.position - _back.transform.position; //вектор фронтальной тяги
            var maneurForceVector = _right.transform.position - _left.transform.position; //вектор боковой тяги
            _thrustForce.force = 
                (thrustForceVector.normalized) * (ShipsBrain.GetThrustSpeed() + ShipsBrain.GetStraightManeurSpeed()) +
                (maneurForceVector.normalized) * ShipsBrain.GetSideManeurSpeed();
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