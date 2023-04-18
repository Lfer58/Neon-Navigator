using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // A trigger object based at the start of the levels will change this value to update spawn points.
    public Vector3 respawnPoint;
    private CameraController viewer;
    private PathCreation path;
    public float deadHeight = -2;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        path = GameObject.FindGameObjectWithTag("Player").GetComponent<PathCreation>();
    }

    // Update is called once per frame
    void Update()
    {
        viewer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        if (!path.isPuzzleLevel) {
            if(transform.position.y < viewer.transform.position.y + deadHeight){
                transform.position = respawnPoint;
            }
            if(transform.position.y < viewer.transform.position.x + deadHeight){
                transform.position = respawnPoint;
            }
        } else {
            if(transform.position.y < deadHeight){
                transform.position = respawnPoint;
            }
        }
    }
}
