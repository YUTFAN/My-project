// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rb; // Reference to the Rigidbody component of Player
    public float moveSpeed = 5f; // Speed of player movement
    public float jumpForce = 8f; // Force of the jump(hight)
    public float rotationSpeed = 3f; // Rotation speed
    private bool isGrounded; // Check if player on ground
    public Vector3 facingDirection; // Player facing
    public Camera mainCamera; // Camera facing

    
    // Start is called before the first frame update
    void Start(){
        // Get Player Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Log message of player status
        Debug.Log("Player Successfully Loaded");

        // If the camera is not assigned, automatically find the main camera
        if (mainCamera == null){
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    private void Update(){

        // Get player facing and camera facing 
        facingDirection = transform.forward;

        // Handle movement input direction relative to the player's facing direction
        float horizontal = Input.GetAxis("Horizontal"); // A/D 
        float vertical = Input.GetAxis("Vertical"); // W/S 

        // Get camera facing variables
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Calculate input direction relative to the player's facing direction
        Vector3 moveDir = cameraForward * vertical + cameraRight * horizontal;
        moveDir.Normalize();

        // Rotate player to face the camera's direction if moving
        if (moveDir != Vector3.zero){
            PlayerRotation(cameraForward);
        }

        // Apply movement by assign velocity to player rb
        Vector3 moveVelocity = moveDir * moveSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // Apply jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    // Manage player rotation
    void PlayerRotation(Vector3 cameraDirection){
        // Calculate the target rotation to face the camera's forward direction
        Quaternion targetRotation = Quaternion.LookRotation(cameraDirection);

        // Smoothly interpolate the player's rotation towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Player Ground collision checker
    private void OnCollisionEnter(Collision collision){
        // Check if the player is on the ground
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    // Player Ground collision checker
    private void OnCollisionExit(Collision collision){
        // Check if the player has left the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
