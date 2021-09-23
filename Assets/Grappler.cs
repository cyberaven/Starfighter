using System;
using System.Linq;
using UnityEngine;


public class Grappler : MonoBehaviour
{
    public GameObject owner;
    public FixedJoint joint =null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (!joint) return;
        Debug.unityLogger.Log($"{joint.currentForce.magnitude}:{joint.currentTorque.magnitude}");
        // if (joint.breakForce < joint.currentForce.magnitude)
        // {
        //     Debug.unityLogger.Log($"JOINT BREAKS!!! {joint.breakForce}");
        //     Destroy(joint);
        // }
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
        
        // joint.useSpring = true;
        // joint.spring = new JointSpring()
        // {
        //     spring = 100
        // };
        owner.GetComponent<HingeJoint>().connectedMassScale = 1;
        Debug.unityLogger.Log(joint);
    }
}
