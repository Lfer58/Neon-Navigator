using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // A trigger object based at the start of the levels will change this value to update spawn points.
    public Vector3 respawnPoint;
    private CameraController viewer;
    private PlayerController player;
    private PathCreation path;
    public float deadHeight;
    public float constantDeadHeight; //Can't be manipulated by any other class, and seeks to exists as a hard floor for death.
    public bool isConstantApplicable = true; //Flexibility of height is still maintained within movement levels
    public float baseWalkSpeed;
    public float baseCameraSpeed;
    public bool isReseted = false;
    public float baseEnergy;
    private LineEnergy battery;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        path = GameObject.FindGameObjectWithTag("Player").GetComponent<PathCreation>();
        viewer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        battery = GameObject.FindGameObjectWithTag("EnergyCount").GetComponent<LineEnergy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!path.isPuzzleLevel) {
            if(transform.position.y < viewer.transform.position.y + deadHeight){ // Always make sure that dead heights in camera triggers are appropriate to not mess
                                                                                    // this up.
                death();
            }
            if(transform.position.x < viewer.transform.position.x + deadHeight){
                death();
            }
        } else {
            if(transform.position.y < deadHeight){
                death();
            }
        }

        if (transform.position.y < constantDeadHeight && isConstantApplicable) {
            death();
        }

        
    }

    public void resetSpeeds() { // speed resets from trigger.
        viewer.speed = baseWalkSpeed;
        player.walkSpeed = baseCameraSpeed;
    }

    public void resetPath() { // delete all instances of created paths
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Path")) {
            Destroy(o);
        }
    }

    public void death () {
        transform.position = respawnPoint;
            viewer.transform.position = respawnPoint;
            resetSpeeds();
            resetPath();
            battery.energy = baseEnergy;
    }
}
