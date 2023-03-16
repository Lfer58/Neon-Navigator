using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrenght;
    public LogicScript logic;
    public bool birdIsAlive = true;

    public float moveInput;
    public bool faceRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        jump();

        horizontalMovement();

        FlipCharacter();

        if (transform.position.y > 16.5 || transform.position.y < -16.5) {
            logic.gameOver();
            birdIsAlive = false;
        }
        
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (birdIsAlive) {
    //         transform.Rotate(0,0,-75);
    //     }
    //     logic.gameOver();
    //     birdIsAlive = false;
    // }

    public void horizontalMovement() {
        if (Input.GetButton("Horizontal")) {
            moveInput = Input.GetAxis("Horizontal");
            myRigidBody.velocity = new Vector3(moveInput * flapStrenght, myRigidBody.velocity.y, 0);
        }
    }

    public void jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, flapStrenght, 0);
        }
    }

    void FlipCharacter()
    {
        // Change direction of player
        if (!faceRight && moveInput > 0)
        {
            Flip();
        }
        else if (faceRight && moveInput < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        faceRight = !faceRight;

        if (!faceRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (faceRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
    }
}
