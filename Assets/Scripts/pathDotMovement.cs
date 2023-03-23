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
    
    public float radiusExtends;
    public float verticalInput;
    
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

        verticalInput = 0;

        pathCreation();

        pathHeight();
    }

    public bool checkRadius() {
        return transform.position.y < playerY + radiusExtends && transform.position.y > playerY + radiusExtends * -1;
    }

    public void pathCreation() {
        Debug.Log(Input.GetAxis("Fire1"));
        if (!(transform.position.x > playerX + radiusExtends)) {
            myRigidBody.velocity = new Vector3(10 * Input.GetAxis("Fire1"), myRigidBody.velocity.y,0);
        }
        if (Input.GetButton("Fire1")) {
            line.positionCount = i + 1;
            line.SetPosition(i++, new Vector3(transform.position.x, transform.position.y - 0.1f, 0));
        }
        if (!Input.GetButton("Fire1")) {
            transform.position = new Vector3(playerX, playerY, 0);
        }
    }

    public void pathHeight() {
        verticalInput = Input.GetAxis("Vertical");
        if (checkRadius()) {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, verticalInput * 10, 0);
            // transform.position = new Vector3 (playerX, transform.position.y, 0);
        }
        if (!Input.GetButton("Vertical")) {
            returnToBaseY();
        }
    }

    public void returnToBaseY() {
        if (transform.position.y > playerY + snapToBaseRange) 
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, -17, 0);
            // transform.position = new Vector3(playerX, transform.position.y, 0);
        } else if (transform.position.y < playerY - snapToBaseRange) 
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 10, 0);
            // transform.position = new Vector3(playerX, transform.position.y, 0);
        } else 
        {
            transform.position = new Vector3(transform.position.x, playerY, 0);
        }
    }
}
