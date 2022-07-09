using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private bool isGrounded;
    private bool isClimbing;

    public float xAcc = 1f;
    public float xSpeed = 0f;
    public float xMaxSpd = 5f;
    public float jumpPower = 10f;

    private int climbingDirection = -1;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            xSpeed += xAcc * Input.GetAxisRaw("Horizontal");
        }
        else if (isGrounded)
        {
            if (xSpeed > 0)
            {
                xSpeed -= 1;
            }
            else
            {
                xSpeed += 1;
            }

            if (Mathf.Abs(xSpeed) < 2) xSpeed = 0;
        }

        xSpeed = Mathf.Clamp(xSpeed, -xMaxSpd, xMaxSpd);
        transform.Translate(xSpeed * Time.deltaTime, 0, 0);
        
        _animator.SetBool("walk", Input.GetAxis("Horizontal") != 0);
        
        if (isClimbing) _rigidbody.velocity = Vector2.down;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                _rigidbody.AddForce((Vector2)Vector3.up * jumpPower, ForceMode2D.Impulse);
                isGrounded = false;
            }
            else if (isClimbing)
            {
                _rigidbody.velocity = new Vector2(0, 0);
                _rigidbody.AddForce((Vector2)(Vector2.up) * jumpPower / 1.2f, ForceMode2D.Impulse);
                isGrounded = false;
                isClimbing = false;
                xSpeed = xMaxSpd * climbingDirection;
            }
        }

        if (_rigidbody.velocity.y > 0 || _rigidbody.velocity.y < 0)
        {
            isGrounded = false;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag + "/" + collision.contacts[0].normal.y);
        
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            
            if (collision.contacts[0].normal.y > 0.8)
            {
                isGrounded = true;
                isClimbing = false;
            }

            if (!isGrounded && _rigidbody.velocity.y < 0 && Mathf.Abs(collision.contacts[0].normal.x) > 0.9 && collision.gameObject.tag == "Ground")
            {
                isClimbing = true;
                Debug.Log("WALL IN!");
                climbingDirection = (int) collision.contacts[0].normal.x;
            }
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isGrounded && _rigidbody.velocity.y < 0.1){
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
            {

                if (collision.contacts[0].normal.y > 0.8)
                {
                    isGrounded = true;
                    isClimbing = false;
                }
                
                if (!isGrounded && _rigidbody.velocity.y < 0 && Mathf.Abs(collision.contacts[0].normal.x) > 0.9 && collision.gameObject.tag == "Ground")
                {
                    isClimbing = true;
                    Debug.Log("WALL IN!");
                    climbingDirection = (int) collision.contacts[0].normal.x;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isClimbing && collision.gameObject.tag == "Ground")
        {
            isClimbing = false;
            Debug.Log("WALL OUT!");
        }
    }
}
