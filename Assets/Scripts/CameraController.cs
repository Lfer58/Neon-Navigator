using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
// Script for having the camera track player movement
// 

public class CameraController : MonoBehaviour
{
    [Tooltip("Tracks the player movement")]
    public Transform player;
    [Tooltip("offsets the camera so its not right up to players face")]
    public Vector3 offset;

    [Tooltip("How much delay is factored into camera movement")]
    [Range(1,10)]
    public float smoothFactor;

    public bool cameraActivation;
    public CameraTrigger trigger;
    public float speed;

    private void Start() 
    {
        cameraActivation = false;
    }

    private void FixedUpdate()
    {
        if (!cameraActivation) {
            Follow();
        } else {
            transform.position += trigger.movement * Time.deltaTime * speed;
        }
    }

    void Follow()
    {
        Vector3 targetPosition = player.position + offset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);

        transform.position = smoothPosition;
    }

}
