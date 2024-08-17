using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 如果鼠标未解锁，并且没有按住鼠标左键，摄像头才会旋转
        if (Cursor.lockState == CursorLockMode.Locked && !Input.GetMouseButton(0))
        {
            // Get the mouse movement on the X and Y axes
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Adjust the X rotation based on mouseY (inverting it)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the rotation to prevent flipping

            // Apply the rotation to the camera
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX); // Rotate the player body based on mouseX
        }
    }
}
