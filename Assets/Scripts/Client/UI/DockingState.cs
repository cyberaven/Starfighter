using Core;
using Core.InputManager;
using UnityEngine;

namespace Client.UI
{
    public class DockingState : BasePlayerUIElement
    {
        private bool _dockStateUI = false;
        

        public void SwitchState(KeyCode key)
        {
            if (key != InputManager.instance.keyConfig.dock) return;
            
            _dockStateUI = !_dockStateUI;
            
            if (_dockStateUI)
            {
                PlayerScript.gameObject.GetComponent<Camera>().cullingMask |= (1 << 10);
            }
            else
            {
                PlayerScript.gameObject.GetComponent<Camera>().cullingMask &= ~(1 << 10);
            }
        }
    }
}
