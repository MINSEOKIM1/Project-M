using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float velocity = 15f;

    void Start()
    {
        Invoke("Delete", 5f);
    }
    void Update()
    {
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Ground" || col.tag == "Box")
        {
            Debug.Log(col.tag);
            if (col.tag == "Player")
            {
                col.gameObject.GetComponent<TestPlayer>().Damage(10f);
            }

            if (col.tag == "Box")
            {
                col.gameObject.GetComponent<BoxBehaviour>().Damage(20f);
            }
            Destroy(gameObject);
        }
    }
}
