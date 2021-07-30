using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingTrigger : MonoBehaviour
{
    [SerializeField]
    private Boolean _isDocked = false;
    
    
    void OnTriggerEnter(Collider myTrigger) 
    {
        _isDocked = true;
        Debug.unityLogger.Log(_isDocked);
    }

    private void OnTriggerExit(Collider other)
    {
        _isDocked = false;
        Debug.unityLogger.Log(_isDocked);
    }
}
