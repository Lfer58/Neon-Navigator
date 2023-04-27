using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreation : MonoBehaviour
{
    
    public float playerBase;
    private float playerY;
    private float playerX;
    private Rigidbody playerRigidBody;
    private PlayerController player;
    private bool faceRight;
    private Vector3 mousePositionActual;
    
    public float radiusExtends;
    private float distanceFromPlayer;
    public float rotationHeight; //When using up and down, how much the path rotates
    public float creationRestraint; //Angle of restraint for path creation

    public GameObject path;
    private GameObject currentPath; //path instance being generated
    private float pathRotation; // How much the path rotates before extending.
    private float pathRotationBase;
    private float scaleForce;
    private int verticalInput;

    private float positionEnd;
    private float positionEndX;
    private float positionEndY;
    private bool isPathCreated = false;
    private bool isPathVerticalTravel = false;
    
    private PlayerControls playerControls;
    public bool isPuzzleLevel; //Pathing is not constrained to a certain distance if is puzzle level. In future will be changed by triggers.

    public LineEnergy battery;
    private float energyCount;


    // Start is called before the first frame update
    void Start()
    {
        battery = GameObject.FindGameObjectWithTag("EnergyCount").GetComponent<LineEnergy>();
        player = GetComponent<PlayerController>();
    }

    private void OnEnable() {
        if(playerControls == null){
            playerControls = new PlayerControls();
            
        }
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        scaleForce = player.walkSpeed * 2;
        
        playerX = transform.position.x;

        playerY = transform.position.y - playerBase;

        energyCount = battery.energy;

        if (energyCount >= 0) {
            createPathPositions();

            //if (!Input.GetButton("Fire1")) {
            if (!playerControls.Player.Fire.IsPressed()) {
                valueReset();
            }

            setRotation();

            pathCreation();
        } else {
            valueReset();
        }

    }

    public void pathCreation() {
        
        // The second if condition takes advantage of the fact of they order of the functions pathCreation and pathHeight.
        // After letting go of up and down, isPathVerticalTravel isn't negative yet, so this allows us to create a new steady path
        // while not distrubing the whole line, and on calling the pathHeight function, is PathVerticalTravel will become false.
        // This fix was necessary since GetButtonDown only returns true for the initial click, and we need to continue making paths 
        // after we stop altering the height.
        //if ((Input.GetButtonDown("Fire1") && !isPathVerticalTravel) || (isPathVerticalTravel && !Input.GetButton("Vertical"))) {
        if ((playerControls.Player.Fire.WasPressedThisFrame() && !isPathVerticalTravel) || (isPathVerticalTravel && !playerControls.Player.PathMovement.IsPressed())) {
            pathRotation = pathRotationBase;

            // code for straightline creation.
            // isFaceRight();
            // if (faceRight) {
            //     pathRotation = 0;
            // } else {
            //     pathRotation = 180;
            // }
            
            currentPath = Instantiate(path, new Vector3(positionEndX, positionEndY, 0), transform.rotation);
            currentPath.transform.eulerAngles = new Vector3 (0,0, pathRotation);
            isPathCreated = true;
        }

        // Once path is created, it'll scale out linearly with the force in scaleForce
        // It should stop after the path reaches a certain distance, but its not yet implemented.
        if (isPathCreated) {
            // Calculates distance from the base player position so that the path only extends outward to a distance specificied by radiusExtends.
            distanceFromPlayer = (float)Math.Sqrt(Math.Pow(positionEndX - playerX, 2) + Math.Pow(positionEndY - playerY, 2));
            if(isPuzzleLevel) { // Either freeze the character while creating the path, require the player to be stopped, or scrap it and allow movement.
                                // Most important thing that currently needs to be worked upon is how to create the detachment between player and path for the more
                                // elevated creation.
                currentPath.transform.localScale += new Vector3(Time.deltaTime * scaleForce, 0, 0); //Increases the path in the direction of mouse.
            } else {
                if (distanceFromPlayer < radiusExtends) {
                    currentPath.transform.localScale += new Vector3(Time.deltaTime * scaleForce, 0, 0); //Increases the path in the direction of mouse.
                    battery.isPathCreating = true;
                } else {
                    battery.isPathCreating = false;
                }
            }
        }
        pathHeight();
    }
    private void pathHeight() {
        // Only works when path is actually being created.
        //if (isPathCreated && Input.GetButtonDown("Vertical")) {
        if (isPathCreated && playerControls.Player.PathMovement.WasPressedThisFrame()) {
            setVerticalInput();
            if (pathRotation < 90 || pathRotation > 270) {
                pathRotation += rotationHeight * verticalInput;
            } else {
                pathRotation += rotationHeight * verticalInput * -1;
            }
            currentPath = Instantiate(path, new Vector3(positionEndX, positionEndY, 0), transform.rotation);

            // Rotates to the newer path rotation as defined earlier.
            currentPath.transform.eulerAngles = new Vector3 (0,0, pathRotation);
            isPathVerticalTravel = true;
        }
        //if (!Input.GetButton("Vertical")) {
        if (!playerControls.Player.PathMovement.IsPressed()) {
            isPathVerticalTravel = false;
        }
    }

    private void setVerticalInput() {
        var vertDirection = playerControls.Player.PathMovement.ReadValue<Vector2>();
        verticalInput = (int) vertDirection.y;
        /*
        if (Input.GetAxis("Vertical") > 0) {
                verticalInput = 1;
            } else {
                verticalInput = -1;
            }
        */
    }

    private void isFaceRight() {
        faceRight = (transform.eulerAngles.y != 180);

        // if (pathRotation > -90 )
        
        // Code for straightline creation.
    }

    private void createPathPositions() {
        // Calculations of path ending points as the starting points for the new paths.
        // The sin and cos calculations exists for when the paths are angled thus needing the adjustment for x and y positions.
        if (currentPath != null) {
            positionEnd = currentPath.transform.localScale.x;
            if (pathRotation != 0 && pathRotation != 180) {
                // code for mouse creation.
                // similar to setVerticalInput but purely based on direction of pathing for certainty.
                if (currentPath.transform.eulerAngles.z < 180) {
                    verticalInput = 1;
                } else {
                    verticalInput = -1;
                }

                // code for straightline creation
                // setVerticalInput();

                positionEndY = (float)(positionEnd * Math.Abs(Math.Sin(pathRotation * Mathf.Deg2Rad))) * verticalInput + currentPath.transform.position.y;
                positionEndX = (float)(positionEnd * Math.Cos(pathRotation * Mathf.Deg2Rad)) + currentPath.transform.position.x;
            } else {
                positionEndX = currentPath.transform.position.x;
                positionEndY = currentPath.transform.position.y;
            }
        }
    }

    private void valueReset() {
        currentPath = null;
        isPathCreated = false;
        isPathVerticalTravel = false;
        positionEnd = playerX;
        positionEndX = playerX;
        positionEndY = playerY;
        pathRotation = 0;
    }

    private void setRotation() {
        // code for mouse creation.
        // takes in mouse position, and sets the initial rotation based on it.
        //if (Input.GetButtonDown("Fire1")) {
        if (playerControls.Player.Fire.WasPressedThisFrame()) {

            mousePositionActual = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 20f));

            float mouseX = mousePositionActual.x;
            float mouseY = mousePositionActual.y;

            float mousePos = (mouseY - playerY) / (mouseX - playerX);

            pathRotation = (float)(Mathf.Atan2((mouseY - playerY), (mouseX - playerX)) * Mathf.Rad2Deg);

            //Constrained to 30 to -30 degree angles to the right and left of the player, currently only apply for puzzle levels
            //Think about adjusting this becausee player is heavily affected at high enough angles
            // So might still need constraints for the puzzle levels albeit higher. Puzzle trigger to modify this.
            if (!isPuzzleLevel) {
                if (pathRotation > creationRestraint && pathRotation < 180 - creationRestraint) {
                    if (pathRotation < 90) {
                        pathRotation = creationRestraint;
                    } else if (pathRotation > 90) {
                        pathRotation = 180 - creationRestraint;
                    }
                } else if (pathRotation < -1 * creationRestraint && pathRotation > -1 * (180 - creationRestraint)) {
                    if (pathRotation < -90) {
                        pathRotation = -1 * (180 - creationRestraint);
                    } else if (pathRotation > -90) {
                        pathRotation = -1 * creationRestraint;
                    }
                }
            }

            pathRotationBase = pathRotation;
        }
    }
}
