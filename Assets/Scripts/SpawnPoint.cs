using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private const float baseWalkSpeed = 10.0f;
    private const float baseCameraSpeed = 6.0f;
    public bool isReseted = false;
    public float baseEnergy;
    private LineEnergy battery;
    public AudioSource music;
    
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
            if(transform.position.y > viewer.transform.position.y + deadHeight  && !isConstantApplicable && deadHeight < 20 && deadHeight > 0){ 
                // Always make sure that dead heights in camera triggers are appropriate to not mess this up.
                death();
            }
            if(transform.position.y < viewer.transform.position.y + deadHeight  && !isConstantApplicable && deadHeight > -20 && deadHeight < 0){ 
                // Always make sure that dead heights in camera triggers are appropriate to not mess this up.
                death();
            }
            if(transform.position.x < viewer.transform.position.x + deadHeight  && !isConstantApplicable && deadHeight < -20){
                death();
            }
            if(transform.position.x > viewer.transform.position.x + deadHeight  && !isConstantApplicable && deadHeight > 20){
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
        viewer.speed = baseCameraSpeed;
        player.walkSpeed = baseWalkSpeed; 
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
        if (music != null) {
            music.enabled = false;
        }
    }
}
