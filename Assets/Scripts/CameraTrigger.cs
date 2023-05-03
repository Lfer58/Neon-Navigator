using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    public enum Orientation{ Left, Right, Up, Down }
    public Orientation direction;
    public Vector3 movement;
    private CameraController viewer;
    private PlayerController player;
    private SpawnPoint spawn;
    public float cameraSpeed = 1;
    public float playerSpeed = 5;
    public float deadHeight; //Sets how far/up the camera can be of the player before death

    // public GameObject door;


    // Start is called before the first frame update
    void Start()
    {
        viewer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spawn = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnPoint>();
    }

    private void FixedUpdate() 
    {
        //closing doors, doesn't properly work right now because scaling isn't working properly
        // while ((viewer.cameraActivation) && (door.transform.localScale.x < transform.localScale.x)) {
        //     door.transform.localScale += new Vector3(transform.localScale.x, 0, 0);
        // }
    }

    private void OnTriggerEnter(Collider other) {
        // Sets true so the camera then doesn't follow the player and sets the camera movement speed to what is dictated by the autoscroller segment
        viewer.trigger = this;
        viewer.cameraActivation = true;
        viewer.speed = cameraSpeed;
        player.walkSpeed = playerSpeed;
        spawn.deadHeight = deadHeight;
        spawn.isConstantApplicable = false;

        // Depending on direction set on the created object, creates a vector for that direction
        if (direction == Orientation.Up) {
            movement = Vector3.up;
        } else if (direction == Orientation.Down) {
            movement = Vector3.down;
        } else if (direction == Orientation.Right) {
            movement = Vector3.right;
        } else if (direction == Orientation.Left) {
            movement = Vector3.left;
        }
    }

    private void OnTriggerStay(Collider other) {
        // Sets true so the camera then doesn't follow the player and sets the camera movement speed to what is dictated by the autoscroller segment
        if (!viewer.cameraActivation) {
            viewer.trigger = this;
            viewer.cameraActivation = true;
            viewer.speed = cameraSpeed;
            player.walkSpeed = playerSpeed;
            spawn.deadHeight = deadHeight;
            spawn.isConstantApplicable = false;

            // Depending on direction set on the created object, creates a vector for that direction
            if (direction == Orientation.Up) {
                movement = Vector3.up;
            } else if (direction == Orientation.Down) {
                movement = Vector3.down;
            } else if (direction == Orientation.Right) {
                movement = Vector3.right;
            } else if (direction == Orientation.Left) {
                movement = Vector3.left;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        // Allows for the camera to follow the player and destroys the trigger
        spawn.isConstantApplicable = true;
        spawn.resetSpeeds();
        viewer.cameraActivation = false;
        spawn.deadHeight = -10;
        movement = Vector3.zero;
        viewer.trigger = null;
    }
}
