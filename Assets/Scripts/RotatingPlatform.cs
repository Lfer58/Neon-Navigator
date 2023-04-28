using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed;
    public float maxAngle; //leave at zero if want continous spin, and only viable for up to 180
    public bool rotateBack; //Apply only if adding a max angle
    private bool resetToBase = false;
    public enum Orientation{ X, Y, Z}

    public Orientation axis;

    private float baseEulerX;
    private float baseEulerY;
    private float baseEulerZ;

    private float timer;
    public float delay; // delay to come back

    void Start()
    {
        baseEulerX = transform.eulerAngles.x;
        baseEulerY = transform.eulerAngles.y;
        baseEulerZ = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (axis == Orientation.X) {
            if (maxAngle == 0) {
                rotate(true, Vector3.right);
            } else {
                if (setAngle(transform.eulerAngles.x) < maxAngle + baseEulerX && !resetToBase) {
                    rotate(true, Vector3.right);
                } else if (rotateBack) {
                    resetToBase = true;
                }
                if (setAngle(transform.eulerAngles.x) > baseEulerX && resetToBase) {
                    timing(false, Vector3.right);
                } else {
                    resetToBase = false;
                }
            }
        } else if (axis == Orientation.Y) {
            if (maxAngle == 0) {
                rotate(true, Vector3.up);
            } else {
                if (setAngle(transform.eulerAngles.y) < maxAngle + baseEulerY && !resetToBase) {
                    rotate(true, Vector3.up);
                } else if (rotateBack) {
                    resetToBase = true;
                }
                if (setAngle(transform.eulerAngles.y) > baseEulerY && resetToBase) {
                    timing(false, Vector3.up);
                } else {
                    resetToBase = false;
                    timer = 0;
                }
            }
        } else if (axis == Orientation.Z) {
            if (maxAngle == 0) {
                rotate(true, Vector3.forward);
            } else {
                if (setAngle(transform.eulerAngles.z) < maxAngle + baseEulerZ && !resetToBase) {
                    rotate(true, Vector3.forward);
                } else if (rotateBack) {
                    resetToBase = true;
                }
                if (setAngle(transform.eulerAngles.z) > baseEulerZ && resetToBase) {
                    timing(false, Vector3.forward);
                } else {
                    resetToBase = false;
                    timer = 0;
                }
            }
        }
    }

    public void rotate(bool isPositive, Vector3 direction) {
        transform.eulerAngles += direction * Time.deltaTime * rotationSpeed * (isPositive ? 1 : -1);
    }

    public void timing(bool isPositive, Vector3 direction) {
        if (timer < delay) {
            timer += Time.deltaTime;
        } else {
            timer = delay + 1;
            rotate(isPositive, direction);
        }
    }

    private float setAngle (float angle) {
        return (angle > 180) ? angle - 360: angle;
    }
}
