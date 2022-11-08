using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    Vector2 movement;

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }
        
        //animation
        if (movement.x > 0)
        {
            myAnimator.SetBool("isMovingRight", true);
        }else if(movement.x < 0)
        {
            myAnimator.SetBool("isMovingLeft", true);
        }
        else
        {
            myAnimator.SetBool("isMovingLeft", false);
            myAnimator.SetBool("isMovingRight", false);
        }

        if (movement.y > 0)
        {
            myAnimator.SetBool("isMovingUp", true);
        }
        else if (movement.y < 0)
        {
            myAnimator.SetBool("isMovingDown", true);
        }
        else
        {
            myAnimator.SetBool("isMovingDown", false);
            myAnimator.SetBool("isMovingUp", false);
        }



    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
