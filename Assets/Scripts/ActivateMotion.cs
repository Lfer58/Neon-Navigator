using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMotion : MonoBehaviour
{
    public GameObject motion;
    public bool isRotate;
    private RotatingPlatform rotate;
    private MovingPlatform move;
    public float speed;
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        if (isRotate) {
            rotate = motion.GetComponent<RotatingPlatform>();
        } else {
            move = motion.GetComponent<MovingPlatform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        if (isRotate) {
            rotate.rotationSpeed = speed;
            rotate.maxAngle = max;
        } else {
            move.speed = speed;
            move.distance = max;
        }
        Destroy(this);
    }
}
