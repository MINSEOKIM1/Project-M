using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLookMouse : MonoBehaviour
{
    private Vector2 mouse, target;
    private float angle;

    public GameObject _enemyPrefab;
    
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var enemy = Instantiate<GameObject>(_enemyPrefab, transform.position, transform.rotation);
        }
    }
}
