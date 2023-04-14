using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShooter : MonoBehaviour
{
    private const float platformY = -1.5f;

    private LineEnergy lineEnergy;
    private PlayerController movement;
    private GameObject currentPlatform;
    private Transform playerTransform;
    private float platformX = 0.724f;
    private float platformGrowth = 1f;
    private Vector3 platformDirection = new Vector3(1f, 0f, 0f);

    private float screenWidth = 0.5f * Screen.width;
    private float screenHeight = 0.5f * Screen.height;

    
    private Rigidbody playerRigidBody;
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        lineEnergy = Camera.main.GetComponent<LineEnergy>();
        playerTransform = GetComponent<Transform>();
        movement = GetComponent<PlayerController>();
        playerHeight = 3f;
        whatIsGround = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    void Update()
    {

        if (lineEnergy.energyDrained()) {
            return;
        }
         grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);
        
        if (Input.GetMouseButton(0) && currentPlatform is null &&
        (playerRigidBody.GetComponent<Rigidbody>().velocity == new Vector3 (0,0,0)) && grounded) 
        {
            Vector3 playerPos = playerTransform.localPosition;

            currentPlatform = Instantiate(GameObject.Find("GenPlatform"));
            currentPlatform.transform.position = new Vector3(playerPos.x + platformX, playerPos.y + platformY, playerPos.z);
        }
        else if (Input.GetMouseButton(0) && currentPlatform is not null &&
        (playerRigidBody.GetComponent<Rigidbody>().velocity == new Vector3 (0,0,0)) && grounded)
        {
            
            updatePlatformDirection(movement.GetFaceRight());
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentPlatform = null;
            // Allows the creation of a new platform
        }

        if (currentPlatform is not null) {
        // Destroy the platform after 5 seconds
            Destroy(currentPlatform, 5f);
        }

    }

    void updatePlatformDirection(bool facingRight)
    {
        Vector2 mousePos = Input.mousePosition;

        float mouseX = mousePos.x - screenWidth;
        float mouseY = mousePos.y - screenHeight;

        float mouseAngleDegrees = Mathf.Atan2(mouseY, mouseX) * Mathf.Rad2Deg;

        currentPlatform.transform.localRotation = Quaternion.Euler(0, 0, mouseAngleDegrees);
        
        float flipDirection = 1.0f;
        if (!facingRight)
        {
            flipDirection = -1.0f;
        }
        currentPlatform.transform.position += flipDirection * platformDirection * platformGrowth * Time.fixedDeltaTime / 2; // Move the object in the direction of scaling so that the corner on the side stays in place

        currentPlatform.transform.localScale += platformDirection * platformGrowth * Time.fixedDeltaTime; // Scale object in the specified direction
        
    }
}
