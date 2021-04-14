using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    //TODO: допилить скалирование по триггеру
    {
        transform.localScale = new Vector3(Camera.main.orthographicSize / 20, Camera.main.orthographicSize / 20);
    }
}
