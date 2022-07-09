using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    public float speed = 5f;
    public float jumpPower = 10f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        transform.position = transform.position + (
            new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                        0f, 0f));
        
        _animator.SetBool("walk", Input.GetAxis("Horizontal") != 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce((Vector2)Vector3.up * jumpPower, ForceMode2D.Impulse);
        }

        
    }
}
