using Client.Core;
using Core;
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
            if (!other.gameObject.CompareTag(Constants.DockTag) || !gameObject.CompareTag(Constants.DockTag)) return;
            var otherUnit = other.gameObject.GetComponentInParent<UnitScript>();
            if (otherUnit is null) return;
            var isReadyToDock = _state?.GetState() ?? true;
            if (otherUnit.unitConfig.isDockable && PlayerScript.unitConfig.isDockable && isReadyToDock)
            {
                PlayerScript.lastThingToDock = otherUnit;
                PlayerScript.readyToDock = true;
                ClientEventStorage.GetInstance().DockingAvailable.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.unityLogger.Log($"{PlayerScript.gameObject.name}: Dock trigger exit");
            PlayerScript.readyToDock = false;
            ClientEventStorage.GetInstance().DockableUnitsInRange.Invoke();
        }
        
        
    }
}
