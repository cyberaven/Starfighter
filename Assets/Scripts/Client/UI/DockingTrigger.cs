using System;
using Net.Core;
using UnityEngine;

namespace Client.UI
{
    public class DockingTrigger : BasePlayerUIElement
    {
        private GameObject _button;
        public void Start()
        {
            _button = GameObject.Find("LockButton");
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherUnit = other.gameObject.transform.parent.gameObject;
            if (PlayerScript.shipConfig.isDockable)
            {
                PlayerScript.lastThingToDock = otherUnit;
                PlayerScript.readyToDock = true;
                _button.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerScript.readyToDock = false;
            _button.SetActive(false);
        }
        
        
    }
}
