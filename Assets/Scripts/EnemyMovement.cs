using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float normalSpeed, enragedSpeed;
    public float direction { get; private set; }
    private Rigidbody2D _rigidbody;
    private GameObject player;

    public void MovementOnCalm()
    {
        _rigidbody.velocity = new Vector2(direction * normalSpeed, _rigidbody.velocity.y);
    }

    public void MovementOnEnraged()
    {
        float enragedDirection = (player.transform.position.x >= transform.position.x) ? 1.0f : -1.0f;
        _rigidbody.velocity = new Vector2(enragedDirection * enragedSpeed, _rigidbody.velocity.y);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        direction = 1f;
        StartCoroutine("DirectionChange");
    }

    private IEnumerator DirectionChange()
    {
        while (true)
        {
            direction *= -1f;
            player = GameObject.FindGameObjectWithTag("Player");

            float waitDuration = Random.Range(1.5f, 3f);
            yield return new WaitForSeconds(waitDuration);
        }
    }

    private void Update()
    {
        _rigidbody.rotation = 0f;
    }
}
