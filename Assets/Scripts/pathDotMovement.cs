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
    private float playerRotationY;
    private Boolean faceRight;
    
    public float radiusExtends;

    public GameObject cube;
    private GameObject currentPath;
    private float pathRotation;
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
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x;

        playerY = player.transform.position.y - playerBase;

        if (currentPath != null) {
            positionEnd = currentPath.transform.localScale.x;
            if (pathRotation != 0) {
                setVerticalInput();
                positionEndY = (float)(positionEnd * Math.Sin(pathRotation * Math.PI / 180)) * verticalInput + currentPath.transform.position.y;
                Debug.Log(positionEndY);
                isFaceRight();
                positionEndX = (float)(positionEnd * Math.Cos(pathRotation * Math.PI / 180)) * horizontalDirection + currentPath.transform.position.x;
            } else {
                positionEndX = positionEnd + currentPath.transform.position.x;
                positionEndY = currentPath.transform.position.y;
            }
        }

        if (!Input.GetButton("Fire1")) {
            currentPath = null;
            isPathCreated = false;
            isPathVerticalTravel = false;
            positionEnd = playerX;
            positionEndX = playerX;
            positionEndY = playerY;
            pathRotation = 0;
        }

        if (!Input.GetButton("Vertical") && currentPath != null && Math.Abs(currentPath.transform.rotation.z) <= 45) {
            pathRotation = currentPath.transform.rotation.z;
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
        if ((Input.GetButtonDown("Fire1") && !isPathVerticalTravel) || (isPathVerticalTravel && !Input.GetButton("Vertical"))) {
            isFaceRight();
            if (faceRight) {
                pathRotation = 0;
            } else {
                pathRotation = 180;
            }
            currentPath = Instantiate(cube, new Vector3(positionEndX, positionEndY, 0), transform.rotation);
            Debug.Log(pathRotation);
            currentPath.transform.eulerAngles = new Vector3 (0,0, pathRotation);
            isPathCreated = true;
        }
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
        if (isPathCreated && Input.GetButtonDown("Vertical")) {
            isFaceRight();
            if (faceRight) {
                pathRotation += 45;
            } else {
                pathRotation += 135;
            }
            setVerticalInput();
            currentPath = Instantiate(cube, new Vector3(positionEndX, positionEndY, 0), transform.rotation);
            currentPath.transform.eulerAngles = new Vector3 (0,0, pathRotation * verticalInput); //Rotates upwards if positive, or downwards if negative.
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
}
