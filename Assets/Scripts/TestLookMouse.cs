using System;
using UnityEngine;

public class TestLookMouse : MonoBehaviour
{
    private RaycastHit2D hitData;
    private Vector2 mouse, target;
    private float angle;

    public GameObject _enemyPrefab;
    
    private void Shoot()
    {
        var enemy = Instantiate<GameObject>(_enemyPrefab, transform.position, transform.rotation);

        hitData = Physics2D.Raycast(target, (mouse - target).normalized, Mathf.Infinity, (1 << 8) + (1 << 7));
        Debug.DrawRay(target, 100f * (mouse - target), Color.red, 0.1f);

        if (hitData)
        {
            GameObject targeted = hitData.collider.gameObject;
            
            switch (targeted.layer)
            {
                case 7:
                    targeted.GetComponent<LampBehaviour>().Fall();
                    break;

                case 8:
                    targeted.GetComponent<EnemyBehaviour>().Death();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Unhandled Layer Mask Called.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
}
