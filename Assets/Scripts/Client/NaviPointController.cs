using System;
using System.Collections;
using System.Collections.Generic;
using Client.Core;
using Core;
using JetBrains.Annotations;
using Net.PackageData;
using UnityEngine;
using UnityEngine.Events;
using EventType = Net.Utils.EventType;

public class NaviPointController : MonoBehaviour
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
        _point = GameObject.FindGameObjectWithTag(Constants.WayPointTag);
        if (_point is null)
        {
            _point = Instantiate(mainPoint, _position, mainPoint.transform.rotation);
            _point.name = _point.name + Constants.Separator + Guid.NewGuid();
            _point.tag = Constants.WayPointTag;
        }

        _point.transform.position = _position;

        ClientEventStorage.GetInstance().SetPointEvent.Invoke(new EventData()
        {
            data = new WorldObject(mainPoint.name, _point.transform),
            eventType = EventType.WayPointEvent
        });
    }
    
}
