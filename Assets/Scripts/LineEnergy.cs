using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineEnergy : MonoBehaviour
{
    public TMP_Text energyLabel;
    public float energy;
    private const int DRAIN_CONSTANT = 2;

    public bool energyDrained()
    {
        return (int)energy == 0;
    }

    void Update()
    {
        energyLabel.text = "Energy Left: " + (int)energy;
        // Updates energyLabel Text

        if (Input.GetMouseButton(0) && energy > 0)
        {
            energy -= (Time.fixedDeltaTime * DRAIN_CONSTANT);
        }
        // Makes theplayer lose energy if they hold down the left mouse button
        // Needs to not be drained if the path is not being increased further or when it collides with another object
            // The second option might not be need if we make it so that path doesn't extend further when colliding with a wall.
    } 
}
