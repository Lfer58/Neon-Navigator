using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{

    public bool isPuzzleLevel = true;
    private PathCreation path;
    private SpawnPoint spawn;
    public LineEnergy battery;

    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.FindGameObjectWithTag("Player").GetComponent<PathCreation>();
        spawn = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnPoint>();
        battery = GameObject.FindGameObjectWithTag("EnergyCount").GetComponent<LineEnergy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        //Sets what the triggers is marked as to the path creator to change approaches.
        spawn.respawnPoint = transform.position; // respawn point update to the start of new chambers.
        path.isPuzzleLevel = isPuzzleLevel; //sets if puzzle level or not.
        spawn.baseEnergy = battery.energy;
        Destroy(this);
    }
}
