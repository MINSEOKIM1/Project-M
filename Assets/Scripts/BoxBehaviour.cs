using UnityEngine;

class BoxBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool isThrown;

    public void Thrown(Vector3 velocity)
    {
        isThrown = true;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.velocity = velocity;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Subject to be changed
        if (isThrown)
        {
            if (_rigidbody.velocity == Vector2.zero) {_rigidbody.bodyType = RigidbodyType2D.Static;}
        }
        else
        {
            _rigidbody.bodyType = Input.GetKey(KeyCode.Z) ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
        }
    }
}
