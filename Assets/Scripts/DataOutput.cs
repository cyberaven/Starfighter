using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataOutput : MonoBehaviour
{
    // Start is called before the first frame update
    Text pointsText;
    public float rotSpeed;
    private float roundSpeed;
    void Start()
    {
        pointsText = GetComponent<Text>();
        rotSpeed = 0;
        roundSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        roundSpeed = (float)Math.Round(GameObject.Find("Player").GetComponent<playerScript>().Speed, 2); // скорость полета
        rotSpeed = (float)Math.Round(GameObject.Find("Player").GetComponent<playerScript>().Rotation * Mathf.Rad2Deg, 2); //скорость врещения в градусах
        pointsText.text = "Speed: " + roundSpeed.ToString() + "\n" + "Rotation: " + rotSpeed.ToString(); // вывод в UI
    }
}
