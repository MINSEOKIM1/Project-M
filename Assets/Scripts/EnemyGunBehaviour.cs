using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    private GameObject player;

    public void Fire()
    {
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion angleInQuaternion = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        var generatedBullet = Instantiate(_bullet, transform.position, angleInQuaternion);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
