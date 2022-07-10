using System;
using UnityEngine;

class BoxBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool isThrown;
    private BossBehaviour ins;

    public float hp = 100f;

    public void Damage(float damage)
    {
        hp -= damage;
    }
    
    public void Thrown(Vector3 velocity)
    {
        isThrown = true;
        _rigidbody.velocity = velocity;
    }

    public void SetIns(BossBehaviour behaviour)
    {
        ins = behaviour;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            if (ins != null)  ins.boxNum--;
            Destroy(gameObject);
        }
        // Subject to be changed
        if (isThrown)
        {
            if (_rigidbody.velocity == Vector2.zero)
            {
                isThrown = false;
            }
        }
    }
}
