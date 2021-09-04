using System;
using Net.Core;
using UnityEngine;

namespace Client.UI
{
    public class DockingTrigger : BasePlayerUIElement
    {
        private DockingState _state;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public override void Init(PlayerScript playerScript)
        {
            base.Init(playerScript);
            _state = FindObjectOfType<DockingState>();
            Debug.unityLogger.Log($"{PlayerScript.gameObject.name}: Dock trigger INIT {_state}");
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.unityLogger.Log($"{PlayerScript.gameObject.name}: Dock trigger enter {other.gameObject.name}");
            //TODO: выполняться только в случае, если коллизия со сферой
            var otherUnit = other.gameObject.GetComponentInParent<PlayerScript>();
            if (otherUnit is null) return;
            var isReadyToDock = _state?.GetState() ?? true;
            if (otherUnit.shipConfig.isDockable && PlayerScript.shipConfig.isDockable && isReadyToDock)
            {
                PlayerScript.lastThingToDock = otherUnit;
                PlayerScript.readyToDock = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.unityLogger.Log($"{PlayerScript.gameObject.name}: Dock trigger exit");
            PlayerScript.readyToDock = false;
        }
        
        
    }
}
