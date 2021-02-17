using Net.PackageData.EventsData;
using UnityEngine;

namespace Client.Movement
{   
    public class PlayerControl: IMovementAdapter
    {
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
            return Input.GetAxis("Jump") * 2.5f;
        }

        public float GetSideManeurSpeed()
        { 
            return Input.GetAxis("Horizontal");
        }

        public float GetStraightManeurSpeed()
        { 
            return Input.GetAxis("Vertical");
        }

        public float GetShipAngle()
        { 
            return Input.GetAxis("Rotation");
        }

        public void UpdateMovementActionData(MovementEventData data)
        {
            return;
        }
    }
}