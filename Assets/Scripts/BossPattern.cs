using System.Collections;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] private GameObject _boxPrefab, _bulletPrefab;
    [SerializeField] private float boxSpeed, bulletSpeed;
    private BossBehaviour _boss;
    public float patternDuration;
    private Rigidbody2D _rigidbody;
    private GameObject player;

    public IEnumerator IdlePattern()
    {
        _rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(patternDuration);
    }

    public IEnumerator WanderPattern()
    {
        _rigidbody.velocity = new Vector2(Random.Range(-2f, 2f), _rigidbody.velocity.y);
        yield return new WaitForSecondsRealtime(patternDuration - 0.1f);

        _rigidbody.velocity = Vector2.zero;
        yield break;
    }

    public IEnumerator ThrowPattern()
    {
        for (int i = 0; i < 3; ++i)
        {
            var thrownBox = Instantiate<GameObject>(_boxPrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
            float xDifference = player.transform.position.x - transform.position.x;
            yield return new WaitForEndOfFrame();

            _boss.boxNum++;
            thrownBox.GetComponent<BoxBehaviour>().Thrown(Vector3.Normalize(new Vector2(xDifference, Mathf.Abs(xDifference) * Mathf.Tan(60 * Mathf.Deg2Rad))) * boxSpeed);
            thrownBox.GetComponent<BoxBehaviour>().SetIns(_boss);
            yield return new WaitForSecondsRealtime(patternDuration / 3);
        }

    }

    public IEnumerator ShootPattern()
    {
        for (int i = 0; i < 10; ++i)
        {
            float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion rotationInQuaternion = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            var launchedBullet = Instantiate(_bulletPrefab, transform.position, rotationInQuaternion);
            yield return new WaitForSecondsRealtime(patternDuration / 10);
        }
    }

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boss = GetComponent<BossBehaviour>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
