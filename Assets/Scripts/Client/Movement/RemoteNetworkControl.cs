using Net.PackageData.EventsData;

namespace Client.Movement
{   
    public class RemoteNetworkControl: IMovementAdapter
    {
        private MovementEventData _lastMovement;

        public RemoteNetworkControl()
        {
            _lastMovement = new MovementEventData()
            {
                rotationValue = 0f,
                thrustValue = 0f,
                sideManeurValue = 0f,
                straightManeurValue = 0f
            };
        }
        
        public EngineState getMovement()
        {
            var state = new EngineState();
            if(GetThrustSpeed() != 0)
            {
                state.Thrust = true;
            }
            if(GetShipAngle() < 0)
            {
                state.TopRight = true;
                state.BotLeft = true;
            }
            if(GetShipAngle() > 0)
            {
                state.TopLeft = true;
                state.BotRight = true;
            }
            if(GetStraightManeurSpeed() < 0)
            {
                state.TopLeft = true;
                state.TopRight = true;
            }
            if(GetStraightManeurSpeed() > 0)
            {
                state.BotLeft = true;
                state.BotRight = true;
            }
            if(GetSideManeurSpeed() > 0)
            {
                state.TopLeft = true;
                state.BotLeft = true;
            }
            if(GetSideManeurSpeed() < 0)
            {
                state.TopRight = true;
                state.BotRight = true;
            }
            return state;
        }

        public float GetThrustSpeed()
        { 
            return _lastMovement.thrustValue;
        }

        public float GetSideManeurSpeed()
        { 
            return _lastMovement.sideManeurValue;
        }

        public float GetStraightManeurSpeed()
        { 
            return _lastMovement.straightManeurValue;
        }

        public float GetShipAngle()
        { 
            return _lastMovement.rotationValue;
        }

        public bool GetAnyAction() => false;
        
        public void UpdateMovementActionData(MovementEventData data)
        {
            _lastMovement = data;
        }
    }
}