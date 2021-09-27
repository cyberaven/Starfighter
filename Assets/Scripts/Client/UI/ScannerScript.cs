using System.Collections;
using System.Collections.Generic;
using Client;
using UnityEngine;

namespace Client
{
    public class ScannerScript : MonoBehaviour
    {
        public GameObject ship;
        private GameObject _target;
        
        public void Init(PlayerScript playerScript, GameObject target = null)
        {
            ship = playerScript.gameObject;
            if (target is null)
                _target = ship;
        }

        void Update()
        {
              transform.position = ship.transform.position;
        }
    }
}