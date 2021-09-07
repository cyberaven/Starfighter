using System;
using System.Collections.Generic;
using Client.Core;
using UnityEngine;

namespace Client
{
    public class DockCheckZone: MonoBehaviour
    {
        private List<GameObject> _objectsInDockZone = new List<GameObject>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<UnitScript>().unitConfig.isDockable)
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