using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float normalSpeed, enragedSpeed;
    public float direction { get; private set; }
    private Rigidbody2D _rigidbody;
    private GameObject player;
    private RaycastHit2D hitData;

    public void MovementOnCalm()
    {
        _rigidbody.velocity = new Vector2(direction * normalSpeed, _rigidbody.velocity.y);
    }

    public void MovementOnEnraged()
    {
        float enragedDirection = (player.transform.position.x >= transform.position.x) ? 1.0f : -1.0f;
        _rigidbody.velocity = new Vector2(enragedDirection * enragedSpeed, _rigidbody.velocity.y);
    }

    private void Antifall()
    {
        hitData = Physics2D.Raycast(transform.position + new Vector3(0.4f * direction, 0f), Vector2.down, 1f, 1 << 6);
        // Debug.DrawRay(transform.position + new Vector3(0.4f * direction, 0f), Vector3.down, Color.red);

        if (!hitData)
        {
            direction *= -1f;
        }
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

            float waitDuration = Random.Range(1.5f, 10f);
            yield return new WaitForSeconds(waitDuration);
        }
    }

    private void Update()
    {
        Antifall();
        _rigidbody.rotation = 0f;
    }
}