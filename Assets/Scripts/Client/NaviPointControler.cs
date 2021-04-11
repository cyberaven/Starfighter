using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class NaviPointControler : MonoBehaviour
{
    public GameObject mainPoint;
    private GameObject _point;
    private Vector3 _clickCoords; 
    private Vector3 _position;

    private void OnGUI()
    {
        if (Event.current.button == 0 && Event.current.isMouse)
            SetPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void SetPoint(Vector3 _clickCoords)
    {
        _position = new Vector3(_clickCoords.x, 0, _clickCoords.z);
        _point = GameObject.FindGameObjectWithTag("CoursePoint") ?? Instantiate(mainPoint, _position, mainPoint.transform.rotation);
        
        _point.transform.position = _position;
    }
    
}
