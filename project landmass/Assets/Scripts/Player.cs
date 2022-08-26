using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject cat;
    Animator myAnim;
    Rigidbody2D rb;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 600;
        }
        else
        {
            speed = 100;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(cat, transform.position, Quaternion.identity);
        }
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxis("Vertical")) * speed;
        transform.GetChild(0).localScale = new Vector3(-1.5f * Mathf.Sign(rb.velocity.x), 1.5f, 1.5f);
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            myAnim.Play("player_walk");
        }
        else
        {
            myAnim.Play("player_still");

        }

    }
}
