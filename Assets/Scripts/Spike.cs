using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private SpawnPoint spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        spawn.death();
    }
}
