using UnityEngine;

namespace Client
{   
    public class PlayerControl: IMovementAdapter
    {
         public EngineState getMovement()
         {
            var state = new EngineState();
            if(Input.GetAxis("Jump") != 0)
            {
                state.Thrust = true;
            }
            if(Input.GetAxis("Rotation") < 0)
            {
                state.TopRight = true;
            }
            if(Input.GetAxis("Rotation") > 0)
            {
                state.TopLeft = true;
            }
            if(Input.GetAxis("Vertical") < 0)
            {
                state.TopLeft = true;
                state.TopRight = true;
            }
            if(Input.GetAxis("Vertical") > 0)
            {
                state.BotLeft = true;
                state.BotRight = true;
            }
            if(Input.GetAxis("Horizontal") > 0)
            {
                state.TopLeft = true;
                state.BotLeft = true;
            }
            if(Input.GetAxis("Horizontal") < 0)
            {
                state.TopRight = true;
                state.BotRight = true;
            }
            return state;
        }

         public float GetThrustSpeed()
         {
             return Input.GetAxis("Jump") * 2.5f + Input.GetAxis("Vertical");
         }
         
         public float GetManeurSpeed()
         {
             return Input.GetAxis("Horizontal");
         }

         public float GetShipAngle()
         {
             return Input.GetAxis("Rotation");
         }
    }
}