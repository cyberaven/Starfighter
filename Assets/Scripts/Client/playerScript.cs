using UnityEngine;
using UnityEngine.Serialization;

namespace Client
{
    public class PlayerScript : MonoBehaviour
    {
        private GameObject _front, _back, _left, _right;
        private Rigidbody _ship, _engine;
        private ParticleSystem _tlm, _trm, _blm, _brm, _te;
        private ConstantForce _thrustForce;
        public float shipSpeed, shipRotation;
        [SerializeField]
        private Vector3 thrustForceVector;
        [SerializeField]
        private Vector3 maneurForceVector;
        private IMovementAdapter _shipsBrain;

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
            _shipsBrain = new PlayerControl();
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
            _thrustForce.force = ((thrustForceVector / thrustForceVector.magnitude) * _shipsBrain.GetThrustSpeed()) +
                                ((maneurForceVector / maneurForceVector.magnitude) * _shipsBrain.GetManeurSpeed());
            _thrustForce.torque = new Vector3(0, _shipsBrain.GetShipAngle(), 0);
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
            
            var engines = _shipsBrain.getMovement();
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