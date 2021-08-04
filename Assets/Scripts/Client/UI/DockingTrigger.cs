using Net.Core;
using UnityEngine;

namespace Client.UI
{
    public class DockingTrigger : BasePlayerUIElement
    {
        
        private void OnTriggerEnter(Collider other)
        {
            var otherUnit = other.gameObject.GetComponentInParent<PlayerScript>();
            if (otherUnit.shipConfig.isDockable && PlayerScript.shipConfig.isDockable)
            {
                PlayerScript.lastThingToDock = otherUnit;
                otherUnit.readyToDock = true;
                PlayerScript.readyToDock = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            other.gameObject.GetComponentInParent<PlayerScript>().readyToDock = false;
            PlayerScript.readyToDock = false;
        }
        
        
    }
}
