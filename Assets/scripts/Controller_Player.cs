﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public float speed = 10;
    public float boostSpeed = 2;
    public bool boostOn = false;

    private Rigidbody rb;

    public GameObject projectile;
    public GameObject doubleProjectile;
    public GameObject missileProjectile;
    public GameObject laserProjectile;
    public GameObject option;
    public int powerUpCount=0;

    internal bool doubleShoot;
    internal bool missiles;
    internal float missileCount;
    internal float shootingCount=0;
    internal bool forceField;
    internal bool laserOn;

    public static bool lastKeyUp;

    public delegate void Shooting();
    public event Shooting OnShooting;

    private Renderer render;

    internal GameObject laser;

    //private List<Controller_Option> options;
    
    public static Controller_Player _Player;
    
    private void Awake()
    {
        if (_Player == null)
        {
            _Player = GameObject.FindObjectOfType<Controller_Player>();
            if (_Player == null)
            {
                GameObject container = new GameObject("Player");
                _Player = container.AddComponent<Controller_Player>();
            }
            //Debug.Log("Player==null");
          //  DontDestroyOnLoad(_Player);
        }
        else
        {
            //Debug.Log("Player=!null");
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        powerUpCount = 0;
        doubleShoot = false;
        missiles = false;
        laserOn = false;
        forceField = false;
        //options = new List<Controller_Option>();
    }

    private void Update()
    {
        CheckForceField();
        ActionInput();
        
    }    

    private void CheckForceField()
    {
        if (forceField)
        {
            render.material.color = Color.blue;
        }
        else
        {
            render.material.color = Color.red;
        }
    }

    public virtual void FixedUpdate()
    {
        Movement();
    }

    public virtual void ActionInput()
    {
        missileCount -= Time.deltaTime;
        shootingCount -= Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space) && shootingCount < 0)
        {
            if (OnShooting != null)
            {
                OnShooting();
            }

            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            shootingCount = 0.10f;
        }
    }


    private void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed* inputX,speed * inputY);
        rb.velocity = movement;
        
        if (Input.GetKey(KeyCode.W))
        {
            lastKeyUp = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            lastKeyUp = false;
        }
    }

     void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("SpeedItem"))
        {
            IncreaseSpeed();
            Destroy(collision.gameObject);
        }
    }

    void IncreaseSpeed()
    {
        speed = boostSpeed * speed;
        boostOn = true;
    }

    

    

}
