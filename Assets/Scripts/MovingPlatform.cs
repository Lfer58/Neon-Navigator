using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private Vector3 movement;
    public float distance;
    public float speed = 1;
    private Vector3 basePosition;
    private float platformX;
    private float platformY;
    private Vector3 destinationPosition;
    public enum Orientation{ Left, Right, Up, Down }
    public Orientation direction;
    private bool resetToBase = false;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        // Depending on direction set on the created object, creates a vector for that direction
        if (direction == Orientation.Up) {
            movement = new Vector3(0, 1, 0);
            destinationPosition = new Vector3(basePosition.x, basePosition.y +  distance, 0);
        } else if (direction == Orientation.Down) {
            movement = new Vector3(0, -1, 0);
            destinationPosition = new Vector3(basePosition.x, basePosition.y - distance, 0);
        } else if (direction == Orientation.Right) {
            movement = new Vector3(1, 0, 0);
            destinationPosition = new Vector3(basePosition.x + distance, basePosition.y, 0);
        } else if (direction == Orientation.Left) {
            movement = new Vector3(-1, 0, 0);
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
                move(true);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformY > basePosition.y) {
                move(false);
            } else {
                resetToBase = false;
            }
        } else if (direction == Orientation.Down) {
            if (platformY > destinationPosition.y && !resetToBase) {
                move(true);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformY < basePosition.y) {
                move(false);
            } else {
                resetToBase = false;
            }
        } else if (direction == Orientation.Right) {
            if (platformX < destinationPosition.x && !resetToBase) {
                move(true);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformX > basePosition.x) {
                move(false);
            } else {
                resetToBase = false;
            }
        } else if (direction == Orientation.Left) {
            if (platformX > destinationPosition.x && !resetToBase) {
                move(true);
            } else {
                resetToBase = true;
            }
            if (resetToBase && platformX < basePosition.x) {
                move(false);
            } else {
                resetToBase = false;
            }
        }
    }

    public void move(bool isPositive) {
        transform.position += movement * Time.deltaTime * speed * (isPositive ? 1 : -1);
    }
}
