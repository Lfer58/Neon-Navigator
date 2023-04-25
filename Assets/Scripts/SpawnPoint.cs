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
    public float deadHeight = -2;
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
                transform.position = respawnPoint;
                viewer.transform.position = respawnPoint;
                resetSpeeds();
                resetPath();
                battery.energy = baseEnergy;
            }
            if(transform.position.x < viewer.transform.position.x + deadHeight){
                transform.position = respawnPoint;
                viewer.transform.position = respawnPoint;
                resetSpeeds();
                resetPath();
                battery.energy = baseEnergy;
            }
        } else {
            if(transform.position.y < deadHeight){
                transform.position = respawnPoint;
                resetSpeeds();
                resetPath();
                battery.energy = baseEnergy;
            }
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
}
