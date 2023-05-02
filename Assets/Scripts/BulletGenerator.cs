using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    private const float XOffset = 0.5f;
    private const float YOffset = 1.5f;
    private const float ZOffset = 2.35f;
    private const float timeBetweenShots = 0.5f;

    private float timeSinceLastShot = 0.0f;
    private Transform turret;
    
    void Start()
    {
        turret = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float elapsedTime = Time.time - timeSinceLastShot;
        // Calculates the time since the last shot was fired

        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (elapsedTime >= timeBetweenShots && bullet != null)
        {
            bullet.transform.position = new Vector3(turret.position.x + XOffset, turret.position.y + YOffset, turret.position.z + ZOffset);
            bullet.SetActive(true);
            // Activates a bullet instance from the object pool to be seen on screen

            timeSinceLastShot = Time.time;
            // Resets the time since the last shot was fired
        }
    }

}
