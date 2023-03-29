using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathDotMovement : MonoBehaviour
{
    
    public PlayerController player;
    public Rigidbody myRigidBody;
    
    public float playerBase;
    private float playerY;
    private float playerX;
    private Quaternion playerRotation;
    private Boolean faceRight;
    
    public float radiusExtends;

    public GameObject cube;
    private GameObject currentPath; //path instance being generated
    private float pathRotation; // How much the path rotates before extending.
    public float scaleForce;
    private float pathInversion;
    private int verticalInput;
    private int horizontalDirection;

    private float positionEnd;
    private float positionEndX;
    private float positionEndY;
    private Boolean isPathCreated = false;
    private Boolean isPathVerticalTravel = false;
    
    public lineScript lineObject;
    private LineRenderer line;
    private int i = 0;
    

    public float snapToBaseRange;

    // Start is called before the first frame update
    void Start()
    {
        line = lineObject.GetComponent<LineRenderer>();

        playerRotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x;

        playerY = player.transform.position.y - playerBase;

        createPathPositions();

        valueReset();

        if (!Input.GetButton("Vertical") && currentPath != null) {
            pathRotation = currentPath.transform.eulerAngles.z;
        }

        pathCreation();
    }

    public bool checkRadius() {
        return transform.position.y < playerY + radiusExtends && transform.position.y > playerY + radiusExtends * -1;
    }

    public void pathCreation() {
        if (!(transform.position.x > playerX + radiusExtends)) {
            myRigidBody.velocity = new Vector3(10 * Input.GetAxis("Fire1"), myRigidBody.velocity.y,0);
        }
        // The second if condition takes advantage of the fact of they order of the functions pathCreation and pathHeight.
        // After letting go of up and down, isPathVerticalTravel isn't negative yet, so this allows us to create a new steady path
        // while not distrubing the whole line, and on calling the pathHeight function, is PathVerticalTravel will become false.
        // This fix was necessary since GetButtonDown only returns true for the initial click, and we need to continue making paths 
        // after we stop altering the height.
        if ((Input.GetButtonDown("Fire1") && !isPathVerticalTravel) || (isPathVerticalTravel && !Input.GetButton("Vertical"))) {
            isFaceRight();
            if (faceRight) {
                pathRotation = 0;
            } else {
                pathRotation = 180;
            }
            currentPath = Instantiate(cube, new Vector3(positionEndX, positionEndY, 0), playerRotation);
            currentPath.transform.eulerAngles = new Vector3 (0,0, pathRotation);
            isPathCreated = true;
        }
        // Once path is created, it'll scale out linearly with the force in scaleForce
        // It should stop after the path reaches a certain distance, but its not yet implemented.
        if (isPathCreated) {
            line.positionCount = i + 1;
            line.SetPosition(i++, new Vector3(transform.position.x, transform.position.y - 0.1f, 0));
            currentPath.transform.localScale += new Vector3(Time.deltaTime * scaleForce, 0, 0); //Increases the path in the direction of mouse.
        }
        if (!Input.GetButton("Fire1")) {
            transform.position = new Vector3(playerX, playerY, 0);
        }
        pathHeight();
    }
    private void pathHeight() {
        if (checkRadius()) {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, Input.GetAxis("Vertical") * 10, 0);
        }
        // Only works when path is actually being created.
        if (isPathCreated && Input.GetButtonDown("Vertical")) {
            isFaceRight();
            pathRotation += 45;
            setVerticalInput();
            currentPath = Instantiate(cube, new Vector3(positionEndX, positionEndY, 0), playerRotation);
            // Rotates upwards if positive, or downwards if negative.
            // Two directional multiplicatives as a form of quadrant balancing.
            // Ex. if using the up arrows and facing to the right, the values are 1 and -1 respectively.
                // This then takes the right angle of 180, the added 45, thus making it 225. Then applying
                // the multiplicatives makes it -225, or 135. Thus, quadrant 2, the desired quadrant. 
            currentPath.transform.eulerAngles = new Vector3 (0,0, pathRotation * verticalInput * horizontalDirection);
            isPathVerticalTravel = true;
        }
        if (!Input.GetButton("Vertical")) {
            isPathVerticalTravel = false;
            returnToBaseY();
        }
    }

    public void returnToBaseY() {
        if (transform.position.y > playerY + snapToBaseRange) 
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, -17, 0);
        } else if (transform.position.y < playerY - snapToBaseRange) 
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 10, 0);
        } else 
        {
            transform.position = new Vector3(transform.position.x, playerY, 0);
        }
    }

    private void setVerticalInput() {
        if (Input.GetAxis("Vertical") > 0) {
                verticalInput = 1;
            } else {
                verticalInput = -1;
            }
    }

    private void isFaceRight() {
        faceRight = (player.transform.eulerAngles.y != 180);
        if (faceRight) {
            horizontalDirection = 1;
        } else {
            horizontalDirection = -1;
        }
    }

    private void createPathPositions() {
        // Calculations of path ending points as the starting points for the new paths.
        // The sin and cos calculations exists for when the paths are angled thus needing the adjustment for x and y positions.
        if (currentPath != null) {
            positionEnd = currentPath.transform.localScale.x;
            if (pathRotation != 0 && pathRotation != 180) {
                setVerticalInput();
                positionEndY = (float)(positionEnd * Math.Abs(Math.Sin(pathRotation * Math.PI / 180))) * verticalInput + currentPath.transform.position.y;
                isFaceRight();
                positionEndX = (float)(positionEnd * Math.Abs(Math.Cos(pathRotation * Math.PI / 180))) * horizontalDirection + currentPath.transform.position.x;
            } else {
                positionEndX = positionEnd * horizontalDirection + currentPath.transform.position.x;
                positionEndY = currentPath.transform.position.y;
            }
        }
    }

    private void valueReset() {
        if (!Input.GetButton("Fire1")) {
            currentPath = null;
            isPathCreated = false;
            isPathVerticalTravel = false;
            positionEnd = playerX;
            positionEndX = playerX;
            positionEndY = playerY;
            pathRotation = 0;
        }
    }
}
