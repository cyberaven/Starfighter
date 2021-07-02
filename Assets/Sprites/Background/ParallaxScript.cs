using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    MeshRenderer mr;
    private Material mat;
    private Vector2 offset;

    public float ParallaxRate;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        offset = mat.mainTextureOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset.x = transform.position.x / 10f / ParallaxRate;
        offset.y = transform.position.z / 10f / ParallaxRate;
        mat.mainTextureOffset = offset;
    }
}
