using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Weapon
{
    handgun,
    machinegun,
    shotgun
}
public class TestLookMouse : MonoBehaviour
{
    [SerializeField] private GameObject _gunLine;

    private UIManager UIManager = TestGameManager.Instance.UIManager;

    private RaycastHit2D hitData;
    private Vector2 mouse, target;
    private float angle;
    private bool isReloading = false;

    public GameObject _enemyPrefab;

    public Weapon weaponNum = Weapon.machinegun;

    public float shootTime = 0f;
    public float shootDelay = 0.2f;

    public int handGunMaxBullets = 15;
    public int machinGunMaxBullets = 40;
    public int shotGunMaxBullets = 7;

    public int curBullets = 0;

    public Text weaponInfoText;

    private void Shoot()
    {
        curBullets--;
        
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
                if (curBullets > handGunMaxBullets) curBullets = handGunMaxBullets;
                weaponInfoText.text = "Handgun - " + curBullets + "/" + handGunMaxBullets;
                if (Input.GetKeyDown(KeyCode.Mouse0) && curBullets > 0)
                {
                    Shoot();
                }

                if (!isReloading && Input.GetKeyDown(KeyCode.Q))
                {
                    weaponNum = Weapon.machinegun;
                }

                break;
            case Weapon.machinegun:
                if (curBullets > machinGunMaxBullets) curBullets = machinGunMaxBullets;
                weaponInfoText.text = "Machinegun - " + curBullets + "/" + machinGunMaxBullets;
                if (Input.GetKey(KeyCode.Mouse0) && shootTime <= 0 && curBullets > 0)
                {
                    shootTime = shootDelay;
                    Shoot();
                }
                if (!isReloading && Input.GetKeyDown(KeyCode.Q))
                {
                    weaponNum = Weapon.handgun;
                }


                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine("Reload");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2f);
        switch (weaponNum)
        {
            case Weapon.handgun:
                curBullets = handGunMaxBullets;
                break;
            case Weapon.machinegun:
                curBullets = machinGunMaxBullets;
                break;
            default:
                break;
        }

        isReloading = false;
    }
}
