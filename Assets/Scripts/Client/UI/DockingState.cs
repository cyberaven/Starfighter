using Core;
using Core.InputManager;
using UnityEngine;

namespace Client.UI
{
    public class DockingState : BasePlayerUIElement
    {
        private bool _dockStateUI = false;
        [SerializeField] private GameObject _dockingIndicator;
        [SerializeField] private Camera _camera;
        

        public void SwitchState()
        {
            _dockStateUI = !_dockStateUI;
            
            if (_dockStateUI)
            {
                _camera.cullingMask |= (1 << 10);
                _dockingIndicator.SetActive(true);
            }
            else
            {
                _camera.cullingMask &= ~(1 << 10);
                _dockingIndicator.SetActive(false);
            }
        }
    }
}
