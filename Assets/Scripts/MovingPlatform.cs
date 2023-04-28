using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private Vector3 movement;
    public float distance;
    public float speed = 1;
    public float secondarySpeed = -1;
    private Vector3 basePosition;
    private float platformX;
    private float platformY;
    private Vector3 destinationPosition;
    public enum Orientation{ Left, Right, Up, Down }
    public Orientation direction;
    private bool resetToBase = false;
    public float delay;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        timer = 0;
        
        if (secondarySpeed == -1) { //sets secondary speed to speed, as a way to naturalize speeds if wanted same speeds.
            secondarySpeed = speed;
        }
        // Depending on direction set on the created object, creates a vector for that direction
        if (direction == Orientation.Up) {
            movement = Vector3.up;
            destinationPosition = new Vector3(basePosition.x, basePosition.y +  distance, 0);
        } else if (direction == Orientation.Down) {
            movement = Vector3.down;
            destinationPosition = new Vector3(basePosition.x, basePosition.y - distance, 0);
        } else if (direction == Orientation.Right) {
            movement = Vector3.right;
            destinationPosition = new Vector3(basePosition.x + distance, basePosition.y, 0);
        } else if (direction == Orientation.Left) {
            movement = Vector3.left;
            destinationPosition = new Vector3(basePosition.x - distance, basePosition.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        platformX = transform.position.x;
        platformY = transform.position.y;

        moving();
    }

    public void moving() {
        if (direction == Orientation.Up) {
            if (platformY < destinationPosition.y && !resetToBase) {
                move(true, speed);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformY > basePosition.y) {
                timing(false, secondarySpeed);
            } else {
                resetToBase = false;
                timer = 0;
            }
        } else if (direction == Orientation.Down) {
            if (platformY > destinationPosition.y && !resetToBase) {
                move(true, speed);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformY < basePosition.y) {
                timing(false, secondarySpeed);
            } else {
                resetToBase = false;
                timer = 0;
            }
        } else if (direction == Orientation.Right) {
            if (platformX < destinationPosition.x && !resetToBase) {
                move(true, speed);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformX > basePosition.x) {
                timing(false, secondarySpeed);
            } else {
                resetToBase = false;
                timer = 0;
            }
        } else if (direction == Orientation.Left) {
            if (platformX > destinationPosition.x && !resetToBase) {
                move(true, speed);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformX < basePosition.x) {
                timing(false, secondarySpeed);
            } else {
                resetToBase = false;
                timer = 0;
            }
        }
    }

    public void move(bool isPositive, float speed) {
        transform.position += movement * Time.deltaTime * speed * (isPositive ? 1 : -1);
    }

    public void timing(bool isPositive, float speed) {
        if (timer < delay) {
            timer += Time.deltaTime;
        } else {
            timer = delay + 1;
            move(isPositive, speed);
        }
    }
}
