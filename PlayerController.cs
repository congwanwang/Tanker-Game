using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    string openID;

    private float nextFire;
    Rigidbody2D rb;

    float horizontalInput;
    float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Fire();
        }       
    }

    void FixedUpdate()
    {         
        Vector3 movement = new Vector3(horizontalInput, verticalInput,0.0f);
        rb.velocity = movement*speed;

        rb.position = new Vector3
        (

            Mathf.Clamp(rb.position.x,BoundaryInformation.xMin, BoundaryInformation.xMax),
            Mathf.Clamp(rb.position.y, BoundaryInformation.yMin, BoundaryInformation.yMax),
            0.0f         
        );
        if(rb.velocity.magnitude > 0.01f)
        {
            rb.rotation = (Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.y, rb.velocity.x));
        }     
    }

    public void PressButtonOne()
    {
        Fire();
    }

    public void Move(float x, float y)
    {
        horizontalInput = x;
        verticalInput = y;
    }

    void Fire()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            bullet.GetComponent<Bullet>().SetController(this);
        }
    }

    public void setOpenID(string openID)
    {
        this.openID = openID;
    }

    public string getOpenID()
    {
        return openID;
    }

    public void HitEnemy()
    {
        GameController.instance.AddScore(openID, 1);
    }

}
