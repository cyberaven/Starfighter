using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressSphere : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private StressSphereConfig config;
    
    private float damage;

    public delegate void StressSphereEnterObjDel(string name);
    public static event StressSphereEnterObjDel StressSphereEnterObjEve;

    public delegate void StressSphereExitObjDel(string name);
    public static event StressSphereExitObjDel StressSphereExitObjEve;

    public delegate void StressSphereStayObjDel(string name, float damage);
    public static event StressSphereStayObjDel StressSphereStayObjEve;


    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = config.Radius;
        damage = config.Damage;
    }

    public void Init(float radius, Vector3 bornPos)
    {
        sphereCollider.radius = radius;
        gameObject.transform.position = bornPos;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ship")//возможно лучше всем сущностям назначить Enum
        {
            StressSphereEnterObjEve?.Invoke("Ship");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ship")//возможно лучше всем сущностям назначить Enum
        {
            StressSphereExitObjEve?.Invoke("Ship");
        }
    }
    public void OnTriggerStay(Collider other)//предлагаю выполнять списание стресса не каждый кадр, а раз в сек. например.
    {
        if (other.tag == "Ship")//возможно лучше всем сущностям назначить Enum
        {
            StressSphereStayObjEve?.Invoke("Ship", damage);
        }
    }
}
