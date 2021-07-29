using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingState : MonoBehaviour
{
    private Boolean _state;

    [SerializeField] private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _state = false;
    }
    
    public void SwitchState()
    {
        if (_state == false)
        {
            _state = true;
            _cam.cullingMask |= (1 << 10);
        }
        else if (_state == true)
        {
            _state = false;
            _cam.cullingMask &= ~(1 << 10);
        }
    }
}
