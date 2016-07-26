﻿using UnityEngine;
using System.Collections;

public class BulletSpan : MonoBehaviour {

    //variable to keep track of how long this bullet has been fired for.
    float timer;
    //boolean to say whether or not this bullet is currently in flight
    bool beingUsed;
    //the type of bullet, either AllyBullet or EnemyBullet 
    string bulletType;
    //bullet target position
    Vector3 target;

    //public set and get methods for timer
    public float Timer { get { return timer; } set { timer = value; } }
    //public set and get methods for beingUsed
    public bool BeingUsed { get { return beingUsed; } set { beingUsed = value; } }
    //public set and get methods for bulletType
    public string BulletType { get { return bulletType; } set { bulletType = value; } }
    //public set and get methods for target
    public Vector3 Target { get { return target; } set { target = value; } }

    // Use this for initialization
    void Start()
    {
        //initialise bulletType to empty string
        bulletType = "";
        //initialised beingUsed to true
        beingUsed = true;
        //initialise timer to 0
        timer = 0.0f;
    }

    void Update()
    {
        //if the bullet is in flight
        if (beingUsed)
        {
            //increase the time the bullet has been flying for
            timer += Time.deltaTime;
            //if the time the bullet has been flying for is greater than 3 seconds
            if (timer >= 3.0f)
            {
                //flag this bullet for removal by the manager
                beingUsed = false;
                //reset the timer
                timer = 0.0f;
            }
        }

        gameObject.transform.forward = (target - gameObject.transform.position);
        gameObject.transform.position += (target - gameObject.transform.position).normalized * Time.deltaTime * 20.0f;
    }

    //when the bullet collides with any collision object
    void OnCollisionEnter(Collision coll)
    {
        if(bulletType == "EnemyBullet")
        {
            if(coll.gameObject.tag == "AllyUnit")
            {
                //flag this bullet for removal by the manager
                beingUsed = false;
                //reset the timer
                timer = 0.0f;
            }
        }
        
        if(bulletType == "AllyUnit")
        {
            if(coll.gameObject.tag == "EnemyUnit")
            {
                //flag this bullet for removal by the manager
                beingUsed = false;
                //reset the timer
                timer = 0.0f;
            }
        }
    }
}