using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : SaiMonoBehaviour
{
    public GameObject arrow;
    public float launchForce;
    public Transform shotPoint;

    protected override void Update()
    {
        base.Update();
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - bowPosition;
        transform.right = direction;

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right*launchForce;

    }
}
