using System;
using System.Collections;
using System.Collections.Generic;
using Client;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grappler : MonoBehaviour
{
    public GameObject owner;
    public GameObject hook;
    public Joint joint;
    public float grapplerLength;
    public LayerMask layerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        var position = owner.transform.position;
        var result = Physics.Raycast(position, other.transform.position - position, out var hit, grapplerLength,
            layerMask);

        joint = new SpringJoint()
        {
            anchor = position,
            enableCollision = false,

        };
    }
}
