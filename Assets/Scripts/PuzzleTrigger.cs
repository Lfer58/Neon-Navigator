using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{

    public bool isPuzzleLevel = true;
    private PathCreation path;
    private SpawnPoint spawn;
    private CameraController viewer;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.FindGameObjectWithTag("Player").GetComponent<PathCreation>();
        spawn = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnPoint>();
        viewer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        //Sets what the triggers is marked as to the path creator to change approaches.
        spawn.respawnPoint = transform.position; // respawn point update to the start of new chambers.
        path.isPuzzleLevel = isPuzzleLevel; //sets if puzzle level or not.
        spawn.cameraSpeedBase = viewer.speed; //Sets base speeds to when chatacter passes this to reset speeds. Might just hard code it in the player and camera to avoid
                                              //redeclaration of variables with each trigger.
        spawn.playerSpeedBase = player.walkSpeed;
    }
}
