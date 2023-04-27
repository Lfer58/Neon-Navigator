using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatform : MonoBehaviour
{

    public float platformSpeed;
    public float spikeSpeed;
    public Transform platform;
    public Transform spike;
    public Transform point1;
    public Transform point2;
    private float startPosZ;
    private LineEnergy lineEnergy;

    int direction = 1;

    // Switches the direction of the platform
    Vector3 currentTarget(){
        if (direction == 1){
            return point1.position;
        }
        else{
            return point2.position;
        }
    }
    
    // Draws a line in between platforms for visual debugging
    private void OnDrawGizmos(){
        if(platform!=null && point1!=null && point2!=null) {
            Gizmos.DrawLine(platform.transform.position, point1.transform.position);
            Gizmos.DrawLine(platform.transform.position, point2.transform.position);
        }
    }

    // Tried to Remove Energy
    private void OnTriggerEnter(Collider Player){
        lineEnergy.energy -= 10;
    }

    private void Start(){
        startPosZ = spike.transform.localScale.z;
        lineEnergy = Camera.main.GetComponent<LineEnergy>();
    }
    private void Update(){

        // Sets the direction of the position
        Vector3 target = currentTarget();
        // Moves platform
        platform.position = Vector3.Lerp(platform.position, target, platformSpeed*Time.deltaTime);
        float distance = (target - (Vector3)platform.position).magnitude; 
        
        // changes the direction of movement if close to point
        if (distance <= 0.1f)
        {
            direction *= -1;
        }

        // Adds spikespeed to y size
        float currentX = spike.transform.localScale.x;
        float currentY = spike.transform.localScale.y;
        float currentZ = spike.transform.localScale.z;
        Vector3 newSpikeScale = new Vector3(currentX, currentY, currentZ+spikeSpeed*Time.deltaTime);
        spike.transform.localScale = newSpikeScale;

        // Changes the direction of y growth
        if (newSpikeScale.z >= (startPosZ + 40) || newSpikeScale.z <= (startPosZ)){
            spikeSpeed *= -1;
        }
        
    }
}
