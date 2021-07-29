using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingTrigger : MonoBehaviour
{
    [SerializeField] private Boolean _isDocked = false;
    void OnTriggerStay(Collider myTrigger) 
    {
        _isDocked = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isDocked = false;
    }
}
