using System;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class CoordinatesUI : BasePlayerUIElement
    {
        private Text _pointsText;
        
        
        void Start()
        {
            _pointsText = GetComponent<Text>();
        }
        
        void Update()
        {
            var coordinates = PlayerScript.transform.position;
            _pointsText.text = "Координаты: x " + Math.Round(coordinates.x,0) + " y " + Math.Round(coordinates.z,0); // вывод в UI
        }
    }
}
