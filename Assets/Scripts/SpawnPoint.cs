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
    public float playerSpeedBase;
    public float cameraSpeedBase;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        path = GameObject.FindGameObjectWithTag("Player").GetComponent<PathCreation>();
        viewer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!path.isPuzzleLevel) {
            if(transform.position.y < viewer.transform.position.y + deadHeight){
                transform.position = respawnPoint;
                viewer.transform.position = respawnPoint;
                resetSpeeds();
            }
            if(transform.position.x < viewer.transform.position.x + deadHeight){
                transform.position = respawnPoint;
                viewer.transform.position = respawnPoint;
                resetSpeeds();
            }
        } else {
            if(transform.position.y < deadHeight){
                transform.position = respawnPoint;
                resetSpeeds();
            }
        }
    }

    public void resetSpeeds() { // speed resets from trigger.
        viewer.speed = cameraSpeedBase;
        player.walkSpeed = playerSpeedBase;
    }
}
