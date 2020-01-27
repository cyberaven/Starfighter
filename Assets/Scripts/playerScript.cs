using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{  
    GameObject Front, Back, Left, Right;
    Rigidbody ship, engine, TLM, TRM, BLM, BRM;
    ConstantForce ThurstForce;
    public float ManeurSpeed, ThurstSpeed;
    public float shipSpeed, shipRotation;
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
        shipSpeed = 0;
        shipRotation = 0;
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
        shipSpeed = ship.velocity.magnitude;
        shipRotation = ship.angularVelocity.magnitude;
        // всякое управляющее говно
        ThurstSpeed = Input.GetAxis("Jump")*2.5f+Input.GetAxis("Vertical");
        ManeurSpeed = Input.GetAxis("Horizontal");
        shipAngle = Input.GetAxis("Rotation");
        
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
        if(Input.GetKey(KeyCode.E))
        {
            Destroy(Instantiate(TopRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.Q))
        {
            Destroy(Instantiate(TopLeftManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.W))
        {
            Destroy(Instantiate(TopLeftManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(TopRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            Destroy(Instantiate(BotLeftManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(BotRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            Destroy(Instantiate(BotLeftManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(TopLeftManeur,ship.transform.position,engine.rotation), 0.2f);
        }
        if(Input.GetKey(KeyCode.D))
        {
            Destroy(Instantiate(BotRightManeur,ship.transform.position,engine.rotation), 0.2f);
            Destroy(Instantiate(TopRightManeur,ship.transform.position,engine.rotation), 0.2f);
        }
    }
}