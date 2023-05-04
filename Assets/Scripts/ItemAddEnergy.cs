using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddEnergy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject energy;
    void Start()
    {
        
    }

    // Delete this object when it collides with the player
    void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.gameObject.tag == "Player")
        {
            energy.GetComponent<LineEnergy>().addEnergy(10);
            Destroy(gameObject);
        }
    }
}
