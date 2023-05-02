using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float SPEED = 12f;
    private const float MAX_HEIGHT = 50f;
    private const float BULLET_DAMAGE = 50f;
    private LineEnergy lineEnergy;


    void Start()
    {
        lineEnergy = GameObject.FindWithTag("EnergyCount").GetComponent<LineEnergy>();
    }

    void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            if (transform.position.y > MAX_HEIGHT)
            {
                gameObject.SetActive(false);
                // Deactivates the bullet when it reaches the max height
            }
            transform.position += transform.up * SPEED * Time.deltaTime;
            // Moves the bullet up at a constant rate
        }
        // Updates the position of the bullet when it is active
    }

    // private void OnTriggerEnter3D(Collider Player){
        
    
}
