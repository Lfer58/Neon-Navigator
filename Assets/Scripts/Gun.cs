using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateX = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotateX + offset, 90f, 180f);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
        }
    }
}
