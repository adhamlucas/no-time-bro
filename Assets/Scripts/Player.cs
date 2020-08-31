using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public float Speed;
    public float JumpForce;
    
    public bool isJumping;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private float horizontalMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move() {
        Vector3 movement = new Vector3(joystick.Horizontal, 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        
        
        if(joystick.Horizontal > 0f) {
            animator.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if(joystick.Horizontal < 0f) {
            animator.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if(joystick.Horizontal == 0f) {
            animator.SetBool("run", false);
        }
    }

    void Jump () {
       if(joystick.Vertical > 0.4f) {
           if (!isJumping) {
               rigidbody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
               isJumping = true;
               animator.SetBool("jump", true);
           }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 8) {
            isJumping = false;
            animator.SetBool("jump", false);
        }
    }  
}
