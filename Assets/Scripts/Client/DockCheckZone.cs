using System.Collections.Generic;
using Client.Core;
using Core;
using UnityEngine;

namespace Client
{
    public class DockCheckZone: MonoBehaviour
    {
        private List<GameObject> _objectsInDockZone = new List<GameObject>();
        public int objectsInZone = 0;
        [SerializeField] 
        private PlayerScript _playerScript;
        
        private void OnTriggerEnter(Collider other)
        {
            if (
                other.gameObject.CompareTag(Constants.DockTag) &&
                other.gameObject.GetComponentInParent<UnitScript>() != _playerScript
                )
            {
                ClientEventStorage.GetInstance().DockableUnitsInRange.Invoke();
                _objectsInDockZone.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _objectsInDockZone.Remove(other.gameObject);
            if (_objectsInDockZone.Count == 0)
            {
                ClientEventStorage.GetInstance().NoOneToDock.Invoke();
            }
        }
    }
}