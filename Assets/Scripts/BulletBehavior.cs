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
}
