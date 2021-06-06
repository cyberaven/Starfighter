using System.Collections;
using System.Collections.Generic;
using Client;
using UnityEngine;

public class RotationWheelScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerScript _ship;
    [SerializeField] private int _rotationModifier;
    
    
    void Start()
    {
        _rotationModifier = 5;
    }

    public void Init(PlayerScript ship)
    {
        _ship = ship;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,-_ship.shipRotation.normalized.y*_ship.shipRotation.magnitude * Mathf.Rad2Deg / _rotationModifier * Time.deltaTime);
    }
}
