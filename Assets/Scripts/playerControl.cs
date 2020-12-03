using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class playerControl: movementAdapter
{
    override public engineState getMovement()
    {
        var state = new engineState();
        state.Thrust = false;
        state.topLeft = false;
        state.topRight = false;
        state.botLeft = false;
        state.botRight = false;
        if(Input.GetAxis("Jump") != 0)
        {
            state.Thrust = true;
        }
        if(Input.GetAxis("Rotation") < 0)
        {
            state.topRight = true;
        }
        if(Input.GetAxis("Rotation") > 0)
        {
            state.topLeft = true;
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            state.topLeft = true;
            state.topRight = true;
        }
        if(Input.GetAxis("Vertical") > 0)
        {
            state.botLeft = true;
            state.botRight = true;
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            state.topLeft = true;
            state.botLeft = true;
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            state.topRight = true;
            state.botRight = true;
        }
        Debug.Log(state);
        Debug.ClearDeveloperConsole();
        return state;
    }
}

//class AIControl: movementAdapter
//{
    //написать что то надо
//}