using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cat : Creature
{

    Vector2 prevPos;
    public GameObject myMapGen;
    protected Animator myAnim;
    public float lastDir;
    Rigidbody2D rb;
    float velocity;
    


    public bool CheckReflection = true; //allows the gameobject to update the cat's direction based on the speed, is used to prevent bugs

    public Cat() :base(new Vector2(0, 0))
    {
        //cat initialized
    }

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
   
    }

    private void Update()
    {
     

        DirectionManagement();
        DecreaseStats();
        WaitUpdateDesire();
        Move();


    }
    private void FixedUpdate()
    {
        prevPos = transform.position;
        if (myMapGen.GetComponent<MapGenerator>().savedColorMap[Math.Abs((int)(transform.position.x / 10)) * myMapGen.GetComponent<MapGenerator>().mapWidth + Math.Abs((int)(transform.position.y / 10))] == Color.blue)
        {
            if(thirst_state < 50)
            {
                Debug.Log("d");
            }
            rb.velocity = -rb.velocity;
            transform.position = prevPos;

            myAnim.Play("Cat_Stand");

        }

    }

    private void DirectionManagement()
    {
        if (Mathf.Abs(rb.velocity.x) < 1f) //probably did that for a reason
        {
            velocity = 1;
        }
        else
        {
            velocity = rb.velocity.x;
        }
        if (CheckReflection)
        {
            transform.localScale = new Vector3(-1.5f * Mathf.Sign(velocity), transform.localScale.y, transform.localScale.z);

        }
        if (rb.velocity.x > 0.1 || rb.velocity.x < -0.1) //prevent z scale confusion in lower values
        {
            lastDir = rb.velocity.x;


        }
    }

    private void DecreaseStats()
    {
        thirst_state -= 3 * Time.deltaTime; //Ded
    }

    private void WaitUpdateDesire()
    {
        update_desire_count -= 5 * Time.deltaTime; //Update desire
        if (update_desire_count < 0)
        {
            update_desire_count = update_desire;
            UpdateCurrentDesire();
        }
    }

    public void ReturnToFirstAnimation()
    {
        Debug.Log("Test");

    }
    public override void SearchForSomething(string desire)
    {
        if (changedDesire)
        {
            myAnim.Play("Searching");
            changedDesire = false;

        }

    }
    public override void WanderAround()
    {
        if (changedDesire)
        {
            myAnim.Play("Wandering");
            changedDesire = false;
        }
 


    }

   

}
