using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    private Camera _camera;

    private Vector3 _translationPoint;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _translationPoint = new Vector3(Input.GetAxis("Horizontal") * Camera.main.orthographicSize / 20,Input.GetAxis("Vertical") * Camera.main.orthographicSize / 20,0);
        _camera.transform.Translate(_translationPoint*-1);
        
    }
}
