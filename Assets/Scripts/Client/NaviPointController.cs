using System;
using Client.Core;
using Core;
using Net.PackageData;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Utils;
using EventType = Net.Utils.EventType;

namespace Client
{
    public class NaviPointController : MonoBehaviour
    {
        public GameObject mainPoint;
        private GameObject _point;
        private Vector3 _clickCoords; 
        private Vector3 _position;

        private void OnGUI()
        {
            if (Event.current.button == (int)MouseButton.RightMouse && Event.current.isMouse)
                SetPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        void SetPoint(Vector3 _clickCoords)
        {
            _position = new Vector3(_clickCoords.x, 0, _clickCoords.z);
            _point = GameObject.FindGameObjectWithTag(Constants.WayPointTag);
            if (_point is null)
            {
                _point = Instantiate(mainPoint, _position, mainPoint.transform.rotation);
                _point.name = _point.name.Replace("(Clone)","") + Constants.Separator + Guid.NewGuid();
                _point.tag = Constants.WayPointTag;
            }

            _point.transform.position = _position;

            ClientEventStorage.GetInstance().SetPointEvent.Invoke(new EventData()
            {
                data = new WorldObject(_point.name, _point.transform),
                eventType = EventType.WayPointEvent
            });
        }
    
    }
}
