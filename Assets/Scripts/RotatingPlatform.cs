using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed;
    public enum Orientation{ X, Y, Z}

    public Orientation axis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (axis == Orientation.X) {
            transform.eulerAngles += new Vector3 (1,0,0) * rotationSpeed * Time.deltaTime;
        } else if (axis == Orientation.Y) {
            transform.eulerAngles += new Vector3 (0,1,0) * rotationSpeed * Time.deltaTime;
        } else if (axis == Orientation.Z) {
            transform.eulerAngles += new Vector3 (0,0,1) * rotationSpeed * Time.deltaTime;
        }
    }
}
