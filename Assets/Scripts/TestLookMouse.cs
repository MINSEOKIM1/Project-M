using System;
using UnityEngine;

public enum Weapon
{
    handgun,
    machinegun,
    shotgun
}
public class TestLookMouse : MonoBehaviour
{
    [SerializeField] private GameObject _gunLine;

    private RaycastHit2D hitData;
    private Vector2 mouse, target;
    private float angle;

    public GameObject _enemyPrefab;

    public Weapon weaponNum = Weapon.machinegun;

    public float shootTime = 0f;
    public float shootDelay = 0.2f;
    
    private void Shoot()
    {
        hitData = Physics2D.Raycast(target, (mouse - target).normalized, Mathf.Infinity, (1 << 8) + (1 << 7) + (1 << 6));
        Debug.DrawRay(target, 100f * (mouse - target), Color.red, 0.1f);

        var lineR = Instantiate(_gunLine, transform.position, Quaternion.identity);
        lineR.GetComponent<LineRendererTest>().DrawLine(transform.position, hitData.point);
        
        if (hitData)
        {
            GameObject targeted = hitData.rigidbody.gameObject;
            
            switch (targeted.layer)
            {
                case 7:
                    targeted.GetComponent<LampBehaviour>().Fall();
                    break;

                case 8:
                    // TODO: Implement some logic to call enemy's death method
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Unhandled Layer Mask Called.");
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (shootTime > 0) shootTime -= Time.deltaTime;
        target = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        switch (weaponNum)
        {
            case Weapon.handgun:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shoot();
                }
                break;
            case Weapon.machinegun:
                if (Input.GetKey(KeyCode.Mouse0) && shootTime <= 0)
                {
                    shootTime = shootDelay;
                    Shoot();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
}
