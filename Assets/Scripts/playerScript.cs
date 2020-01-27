using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
public class playerScript : MonoBehaviour
{  
    GameObject Front, Back, Left, Right;
    Rigidbody ship, engine, TLM, TRM, BLM, BRM;
    ConstantForce ThurstForce;
    public float ManeurSpeed, ThurstSpeed;
    public float Speed, Rotation;
    private float Time, preRotation, postRotation; 
    public GameObject thursts, TopLeftManeur, TopRightManeur, BotLeftManeur, BotRightManeur;
    public float shipAngle;
    public Vector3 ThurstForceVector, ManeurForceVector;
        // Start is called before the first frame update
    void Start()
    {
        // всякое говно при создании объекта
        Front = GameObject.Find("Front");
        Back  = GameObject.Find("Back");
        Left  = GameObject.Find("Left");
        Right = GameObject.Find("Right");
        ship = GetComponent<Rigidbody>();
        ThurstForce = GetComponent<ConstantForce>();
        Speed = 0;
        Rotation = 0;
        preRotation = ship.transform.rotation.y;
        ManeurSpeed = 0f;
        ThurstSpeed = 0f;
        shipAngle = 0f;
        engine = thursts.GetComponent<Rigidbody>();
        TRM = TopRightManeur.GetComponent<Rigidbody>();
        TLM = TopLeftManeur.GetComponent<Rigidbody>();
        BRM = BotRightManeur.GetComponent<Rigidbody>();
        BLM = BotLeftManeur.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        Speed = ship.velocity.magnitude;
        Rotation = ship.GetComponent<Rigidbody>().angularVelocity.magnitude;
        // всякое управляющее говно
        if(Input.GetKeyDown(KeyCode.Space)) // маршевый движок
        {
            ThurstSpeed = 2.5f;
        }
        if(Input.GetKeyUp(KeyCode.Space)) // маршевый движок
        {
            ThurstSpeed = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Keypad8)) // передний маневровый
        {
            ThurstSpeed = -1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad8)) // передний маневровый
        {
            ThurstSpeed = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad2)) // маневровый на корме
        {
            ThurstSpeed = 1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad2))
        {
            ThurstSpeed = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad6)) // маневровый на корме
        {
            ManeurSpeed = -1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad6))
        {
            ManeurSpeed = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad4)) // маневровый на корме
        {
            ManeurSpeed = 1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad4))
        {
            ManeurSpeed = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad9)) // правый маневровый у кабины
        {
            shipAngle += -1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad9))
        {
            shipAngle = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad7)) // левый маневровый у кабины
        {
            shipAngle += 1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad7))
        {
            shipAngle = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad1)) // правый маневровый у кабины
        {
            shipAngle += -1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad1))
        {
            shipAngle = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad3)) // левый маневровый у кабины
        {
            shipAngle += 1f;
        }
        if(Input.GetKeyUp(KeyCode.Keypad3))
        {
            shipAngle = 0;
        }
        
        // всякая хуйня которая считает тягу
        ThurstForceVector = Front.transform.position-Back.transform.position; //вектор фронтальной тяги
        ManeurForceVector = Right.transform.position-Left.transform.position; //вектор боковой тяги
        ThurstForce.force = ((ThurstForceVector/ThurstForceVector.magnitude)*ThurstSpeed)+((ManeurForceVector/ManeurForceVector.magnitude)*ManeurSpeed);
        ThurstForce.torque = new Vector3(0,shipAngle,0);
        engine.rotation = Quaternion.Euler(ship.transform.eulerAngles); //крутит выхлоп с кораблем
        
        // зажигаем и тушим партиклы движков
        if(Input.GetKey(KeyCode.Space))
        {
            Destroy(Instantiate(thursts,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad9))
        {
            Destroy(Instantiate(TopRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad7))
        {
            Destroy(Instantiate(TopLeftManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad3))
        {
            Destroy(Instantiate(BotRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad1))
        {
            Destroy(Instantiate(BotLeftManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad8))
        {
            Destroy(Instantiate(TopLeftManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(TopRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad2))
        {
            Destroy(Instantiate(BotLeftManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(BotRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad4))
        {
            Destroy(Instantiate(BotLeftManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(TopLeftManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Keypad6))
        {
            Destroy(Instantiate(BotRightManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(TopRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
    }
}