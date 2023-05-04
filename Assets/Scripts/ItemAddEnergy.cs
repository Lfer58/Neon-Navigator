using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddEnergy : MonoBehaviour
{
    public bool negativeEnergy = false; 
    public Material negativeEnergyMaterial;
    private const float ENERGY_AMOUNT = 50f;

    private LineEnergy lineEnergy;

    void Start()
    {
        lineEnergy = GameObject.FindWithTag("EnergyCount").GetComponent<LineEnergy>();
        if (negativeEnergy)
        {
            GetComponent<Renderer>().material = negativeEnergyMaterial;
        }
    }

    // Delete this object when it collides with the player
    void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.gameObject.tag != "Player")
        {
            return;
        }
        
        if (negativeEnergy)
        {
            lineEnergy.removeEnergy(ENERGY_AMOUNT);
        }
        else
        {
            lineEnergy.addEnergy(ENERGY_AMOUNT);
        }
    }
}
