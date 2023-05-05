using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingRespawn : MonoBehaviour
{
    private SpawnPoint spawnPoint;

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnPoint>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawnPoint.death();
        }
        // Causes the player to die and respawn at the spawn point if they collide with the ceiling
    }
}
