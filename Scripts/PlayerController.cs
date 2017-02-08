using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax;
}

public class PlayerController : MonoBehaviour {

    public static PlayerController current;
    public float speed;
    public float turnSpeed;
    public float turretSpeed;
    public Transform turret;
    private Quaternion targetRotation;
    public Boundary boundary;
    
    public GameObject shot;
    public GameObject railShot;
    public Transform shotSpawn;
    private float lastFire;
    private Vector3 pointerPosition;
    public TankInput tankInput;
    public AimInput aimInput;

    public GameObject playerExplosion;

    public int ammo;
    public int health;



    public float fireDelay;

    // Use this for initialization
    void Start () {
        current = this;
        lastFire = 0;
    }


  
    void Fire()
    {
        if (lastFire + fireDelay < Time.time && aimInput.Firing())
        {
            lastFire = Time.time;
            if (aimInput.GetDoubleTouch() && ammo > 0)
            {
                AndroidAudioController.current.RailSound();
                Instantiate(railShot, shotSpawn.position, shotSpawn.rotation);
                ammo--;
                GameController.current.UpdateAmmoText();
            }
            else
            {
                AndroidAudioController.current.GunSound();
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            
            
            
        }
        
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyShot"))
        {
            health = health - other.gameObject.GetComponent<BoltMover>().damage;
            GameController.current.UpdateAmmoText();
            Destroy(other.gameObject);
            if (health < 1)
            {
                Instantiate(playerExplosion, transform.position, transform.rotation);
                AndroidAudioController.current.PlayerExplosion();
                Destroy(gameObject);
                GameController.current.GameOver();
            }
        }
    }

        // Update is called once per frame
    void Update () {
        


        float moveHorizontal = tankInput.GetDirection().x;
        //float moveHorizontal = Input.GetAxis("Horizontal");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        //Vector3 facing = transform.rotation * Vector3.forward;
        GetComponent<Rigidbody>().velocity = movement * speed;






        //if (movement!=Vector3.zero)
        //{
        //    targetRotation = Quaternion.LookRotation(-movement);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        //    float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        //    //if (angleDifference<30)
        //    //{
                
        //    //};
        //}

        //pointerPosition = Input.mousePosition;
        pointerPosition = aimInput.GetPosition();


        pointerPosition.z = 10;
        pointerPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
        pointerPosition.y = turret.position.y;
        targetRotation = Quaternion.LookRotation(-(pointerPosition - turret.position).normalized);
        turret.rotation = targetRotation;//Quaternion.Slerp(turret.rotation, targetRotation, Time.deltaTime * turretSpeed);
        //Debug.Log(pointerPosition);

        Fire();


        GetComponent<Rigidbody>().position = new Vector3
        (
           Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
           0.0f,
           0.0f
        );

        GetComponent<Rigidbody>().velocity = new Vector3
        (
           GetComponent<Rigidbody>().velocity.x,
           0.0f,
           0.0f
        );

        //currentRotation = GetComponent<Rigidbody>().rotation.eulerAngles.y;
        //nextRotation = Mathf.MoveTowards(currentRotation, targetRotation, -turnSpeed);
        //Debug.Log(transform.rotation.eulerAngles.y);

        //if (moveHorizontal!=0)
        //{
        //    float targetRotation = Mathf.Sign(moveHorizontal) * 90;
        //    float currentRotation = body.rotation.eulerAngles.y;
        //    float nextRotation = Mathf.MoveTowards(currentRotation, targetRotation, turnSpeed);
        //    Debug.Log(targetRotation + "targetRotation");
        //    Debug.Log(currentRotation + "currentRotation");
        //    Debug.Log(nextRotation + "nextRotation");
        //    body.rotation = Quaternion.Euler(0.0f, nextRotation, 0.0f);

        //    if ((Mathf.Abs(targetRotation) - Mathf.Abs(currentRotation)) < 5)
        //    {
        //        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        //        GetComponent<Rigidbody>().velocity = movement * speed;
        //    };
        //}


    }

    public void increaseAmmo(int value)
    {
        ammo = ammo + value;
    }

    public void increaseHealth(int value)
    {
        health = health + value;
    }
}
