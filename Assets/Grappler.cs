using System;
using System.Linq;
using UnityEngine;


public class Grappler : MonoBehaviour
{
    public GameObject owner;
    private FixedJoint joint;
    private LineRenderer lineRenderer;
    
    // Start is called before the first frame update
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, owner.transform.position);
    }
    
    private void LateUpdate()
    {
        if (!joint) return;
        Debug.unityLogger.Log($"{joint.currentForce.magnitude}:{joint.currentTorque.magnitude}");
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponents<Joint>().Any(x => x.connectedBody == gameObject.GetComponent<Rigidbody>())) return;

        joint = other.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = gameObject.GetComponent<Rigidbody>();
        joint.connectedAnchor = other.GetContact(0).point;
        joint.enableCollision = false;
        joint.breakForce = 100f;
        joint.autoConfigureConnectedAnchor = true;
        joint.axis = Vector3.up;

        owner.GetComponent<HingeJoint>().connectedMassScale = 1;
        Debug.unityLogger.Log(joint);
    }
}
