using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private float maxValue = 100;
    [SerializeField] private float minValue = 0;

    public delegate void StressMaxValueReachDel();
    public static event StressMaxValueReachDel StressMaxValueReachEve;

    public delegate void StressMinValueReachDel();
    public static event StressMinValueReachDel StressMinValueReachEve;

    private void OnEnable()
    {
        StressSphere.StressSphereStayObjEve += StressSphereStayObj;
    }
    private void OnDisable()
    {
        StressSphere.StressSphereStayObjEve -= StressSphereStayObj;
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = maxValue;
        slider.minValue = minValue;
    }

    //логику мини и макс значения надо перенести в "модель" стресса это скорее "вью".
    //надо както распределять кому достанется стресс из всех объектов, кто имеет эту хар-ку.    
    public void ChangeValue(float delta) 
    {
        if(slider.value + delta > maxValue)
        {
            StressMaxValueReach();
            return;
        }
        if (slider.value + delta < minValue)
        {
            StressMinValueReach();
            return;
        }
        slider.value += delta;        
    }

    private void StressMaxValueReach()
    {
        slider.value = maxValue;
        StressMaxValueReachEve?.Invoke();
    }
    private void StressMinValueReach()
    {
        slider.value = minValue;
        StressMinValueReachEve?.Invoke();
    }
    private void StressSphereStayObj(string name, float damage)
    {
        ChangeValue(damage);
    }
}
