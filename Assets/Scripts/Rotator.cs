using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody ship;
    public Vector3 angle;
     void Start()
    {
        ship = GameObject.Find("Player").GetComponent<Rigidbody>();
        angle = ship.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        angle = ship.transform.position + (ship.velocity/(ship.velocity.magnitude+0.01f)) * 8;
        transform.position = angle;
    }
}
