using UnityEngine;
using UnityEngine.UI;
using System;
using Client;

public class CoordinatesUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Text _pointsText;
    private GameObject _ship;

    private void Awake()
    {

    }

    void Start()
    {
        _pointsText = GetComponent<Text>();
    }

    public void Init(PlayerScript ps)
    {
        _ship = ps.gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        var coordinates = _ship.transform.position;
        _pointsText.text = "Координаты: x " + Math.Round(coordinates.x,0) + " y " + Math.Round(coordinates.z,0); // вывод в UI
    }
}
