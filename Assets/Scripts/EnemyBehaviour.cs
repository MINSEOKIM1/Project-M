using System;
using System.Collections;
using UnityEngine;

class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float detectRange;
    private bool enraged;
    private RaycastHit2D hitData;

    private EnemyGunBehaviour gun;
    private EnemyMovement movementController;

    public void Death()
    {
        Destroy(gameObject);
    }

    public void Unrage()
    {
        enraged = false;
        StopCoroutine("EnemyGun");
    }

    private void DetectPlayer()
    {
        hitData = Physics2D.Raycast(transform.position + 0.2f * Vector3.up, new Vector2(movementController.direction*10, 0f), detectRange, (1 << 9) + (1 << 10));
        Debug.DrawRay(transform.position + 0.2f * Vector3.up, new Vector2(movementController.direction*10, 0f), Color.red, 0.1f);
        
        if (hitData)
        {
            GameObject targeted = hitData.collider.gameObject;

            switch (targeted.layer)
            {
                case 9:
                    enraged = true;
                    StartCoroutine("EnemyGun");
                    break;
                case 10:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    // Coroutines

    private IEnumerator EnemyGun()
    {
        while (true)
        {
            gun.Fire();
            yield return new WaitForSeconds(1f);
        }
    }

    private void Start()
    {
        enraged = false;
        gun = GetComponent<EnemyGunBehaviour>();
        movementController = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (enraged)
        {
            movementController.MovementOnEnraged();
        }
        else
        {
            movementController.MovementOnCalm();
            DetectPlayer();
        }
    }
}
