using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineEnergy : MonoBehaviour
{
    public TMP_Text energyLabel;
    public float energy = 300;
    private const int DRAIN_CONSTANT = 2;
    public bool isPathCreating = true; //Set within pathCreation for when path is being created.
    private SpawnPoint spawnPoint;

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnPoint>();
    }


    public bool energyDrained()
    {
        return (int)energy <= 0;
    }

    void Update()
    {
        if (energyDrained())
        {
            spawnPoint.death();
        }
        // If energy is 0, player dies and is respawned at the spawn point

        energyLabel.text = "Energy Left: " + ((int)energy > 0 ? (int)energy : 0);
        // Updates energyLabel Text and caps out at 0

        if (Input.GetMouseButton(0) && energy > 0 && isPathCreating)
        {
            energy -= (Time.fixedDeltaTime * DRAIN_CONSTANT);
        }
        // Makes the player lose energy if they hold down the left mouse button
        // Needs to not be drained if the path is not being increased further or when it collides with another object
            // The second option might not be need if we make it so that path doesn't extend further when colliding with a wall.
    } 

    public void addEnergy(float energyToAdd)
    {
        energy += energyToAdd;
    }

    public void removeEnergy(float energy)
    {
        this.energy -= energy;
    }

    public float getEnergy()
    {
        return energy;
    }
    
}
