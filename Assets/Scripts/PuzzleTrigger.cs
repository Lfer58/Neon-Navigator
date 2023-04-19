using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{

    public bool isPuzzleLevel = true;
    private PathCreation path;
    private SpawnPoint spawn;

    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.FindGameObjectWithTag("Player").GetComponent<PathCreation>();
        spawn = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        //Sets what the triggers is marked as to the path creator to change approaches.
        spawn.respawnPoint = transform.position;
        path.isPuzzleLevel = isPuzzleLevel;
    }
}
