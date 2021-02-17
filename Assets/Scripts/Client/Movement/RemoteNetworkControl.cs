using Net.PackageData.EventsData;

namespace Client.Movement
{   
    public class RemoteNetworkControl: IMovementAdapter
    {
        private MovementEventData _lastMoveAction;
        
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
            return _lastMoveAction.thrustValue;
        }

        public float GetSideManeurSpeed()
        { 
            return _lastMoveAction.sideManeurValue;
        }

        public float GetStraightManeurSpeed()
        { 
            return _lastMoveAction.straightManeurValue;
        }

        public float GetShipAngle()
        { 
            return _lastMoveAction.rotationValue;
        }

        public void UpdateMovementActionData(MovementEventData data)
        {
            _lastMoveAction = data;
        }
    }
}