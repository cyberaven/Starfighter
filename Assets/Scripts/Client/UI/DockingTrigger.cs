using Net.Core;
using UnityEngine;

namespace Client.UI
{
    public class DockingTrigger : BasePlayerUIElement
    {
        
        private void OnTriggerEnter(Collider other)
        {
            var otherUnit = other.gameObject.transform.parent.gameObject;
            if (PlayerScript.shipConfig.isDockable)
            {
                PlayerScript.lastThingToDock = otherUnit;
                PlayerScript.readyToDock = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerScript.readyToDock = false;
        }
        
        
    }
}
