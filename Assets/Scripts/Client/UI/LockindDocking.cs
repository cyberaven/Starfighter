using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Client.UI
{
    
    public class LockindDocking : BasePlayerUIElement
    {
        
        public void Lock()
        {
            if (PlayerScript.GetState() != UnitState.IsDocked)
            {
                PlayerScript.unitStateMachine.ChangeState(UnitState.IsDocked);
            }
            else
            {
                PlayerScript.unitStateMachine.ChangeState(UnitState.InFlight);
            }
        }
    }
}