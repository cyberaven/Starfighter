using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseView : MonoBehaviour
{
GameObject arrow, ship;
Rigidbody shipRB;
    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Player");
        arrow = GameObject.Find("Pointers");
        shipRB = ship.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ship.transform.position;
    }
}
