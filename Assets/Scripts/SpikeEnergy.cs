using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnergy : MonoBehaviour
{
    public float SPIKE_DAMAGE = 50f;
    private LineEnergy lineEnergy;
    
    void Start()
    {
        lineEnergy = GameObject.FindWithTag("EnergyCount").GetComponent<LineEnergy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.gameObject.tag == "Player")
        {
            lineEnergy.removeEnergy(SPIKE_DAMAGE);
        }
    }
}
