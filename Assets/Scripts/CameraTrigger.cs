using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    public string direction;
    public Vector3 movement;
    private CameraController viewer;
    private PlayerController player;
    public float cameraSpeed;
    public float playerSpeed;

    public GameObject door;


    // Start is called before the first frame update
    void Start()
    {
        viewer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate() 
    {
        //closing doors, doesn't properly work right now because scaling isn't working properly
        while ((viewer.cameraActivation) && (door.transform.localScale.x < transform.localScale.x)) {
            door.transform.localScale += new Vector3(transform.localScale.x, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Sets true so the camera then doesn't follow the player and sets the camera movement speed to what is dictated by the autoscroller segment
        viewer.cameraActivation = true;
        viewer.speed = cameraSpeed;
        player.walkSpeed = playerSpeed;

        // Depending on direction set on the created object, creates a vector for that direction
        if (direction.Equals("up")) {
            movement = new Vector3(0, 1, 0);
        } else if (direction.Equals("down")) {
            movement = new Vector3(0, -1, 0);
        } else if (direction.Equals("right")) {
            movement = new Vector3(1, 0, 0);
        } else if (direction.Equals("left")) {
            movement = new Vector3(-1, 0, 0);
        }
    }

    private void OnTriggerExit(Collider other) {
        // Allows for the camera to follow the player and destroys the trigger
        viewer.cameraActivation = false;
        movement = new Vector3(0, 0, 0);
        Destroy(gameObject);
    }
}
