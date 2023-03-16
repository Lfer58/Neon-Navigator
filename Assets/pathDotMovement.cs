using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathDotMovement : MonoBehaviour
{
    
    public BirdScript bird;
    public Rigidbody2D myRigidBody;
    public float birdBase;
    private float birdY;
    public float radiusExtends;
    public bool resetToBase;
    public float verticalInput;
    private float birdX;
    private lineScript lineObject;
    private LineRenderer line;
    public int i = 0;

    private float timer = 0;
    public float spawnRate = 1;
    public bool newLinePosition = true;
    

    public float snapToBaseRange;

    // Start is called before the first frame update
    void Start()
    {
        lineObject = GameObject.FindGameObjectWithTag("Line").GetComponent<lineScript>();
        line = lineObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        birdX = bird.transform.position.x;

        birdY = bird.transform.position.y - birdBase;

        verticalInput = 0;

        pathCreation();

        pathHeight();

        if (timer < spawnRate && !Input.GetButton("Fire1")) {
            lineObject.edges = new List<Vector2>();
            line.positionCount = 0;
            i = 0;
        } else if (timer < spawnRate) {
            timer += Time.deltaTime;
        } else {
            timer = 0;
        }



        // if (Input.GetButton("Vertical") && checkRadius())
        // {
        //     myRigidBody.velocity = Vector2.up * Input.GetAxis("Vertical") * 10;

        // } else if (transform.position.y > birdY + snapToBaseRange || transform.position.y < birdY - snapToBaseRange) 
        // {
        //     if (transform.position.y < birdY) {
        //         myRigidBody.velocity = Vector2.up * 10;
        //     } else {
        //         myRigidBody.velocity = Vector2.up * -10;
        //     }
        // } else if (Input.GetButton("Fire1")) 
        // {
        //     myRigidBody.velocity = Vector2.right * 10;
        // }else 
        // {
        //     transform.position = bird.transform.position - (Vector3.up * birdBase);
        // }

        // transform.position = new Vector3(bird.transform.position.x, transform.position.y, 0);
    }

    public bool checkRadius() {
        return transform.position.y < birdY + radiusExtends && transform.position.y > birdY + radiusExtends * -1;
    }

    public void pathCreation() {
        if (!(transform.position.x > birdX + radiusExtends)) {
            myRigidBody.velocity = new Vector3(10 * Input.GetAxis("Fire1"), myRigidBody.velocity.y,0);
        }
        if (Input.GetButton("Fire1")) {
            line.positionCount = i + 1;
            line.SetPosition(i++, new Vector3(transform.position.x, transform.position.y - 0.1f, 0));
        }
        if (!Input.GetButton("Fire1")) {
            transform.position = new Vector3(birdX, birdY, 0);
        }
    }

    public void pathHeight() {
        verticalInput = Input.GetAxis("Vertical");
        if (checkRadius()) {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, verticalInput * 10, 0);
            // transform.position = new Vector3 (birdX, transform.position.y, 0);
        }
        if (!Input.GetButton("Vertical")) {
            returnToBaseY();
        }
    }

    public void returnToBaseY() {
        if (transform.position.y > birdY + snapToBaseRange) 
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, -17, 0);
            // transform.position = new Vector3(birdX, transform.position.y, 0);
        } else if (transform.position.y < birdY - snapToBaseRange) 
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 10, 0);
            // transform.position = new Vector3(birdX, transform.position.y, 0);
        } else 
        {
            transform.position = new Vector3(transform.position.x, birdY, 0);
        }
    }
}
