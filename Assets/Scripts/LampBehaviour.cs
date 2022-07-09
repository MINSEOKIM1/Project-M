using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public void Fall()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
