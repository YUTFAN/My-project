using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody;

    private float xRotation = 0f;
    private bool hasPressedEnter = false; // 用于追踪是否按下了Enter键
    private bool isCursorLocked = false;  // 用于追踪鼠标锁定状态

    void Start()
    {
        // 初始时不锁定鼠标
        UnlockCursor();
    }

    void Update()
    {
        // 如果按下了Enter键，启用鼠标锁定逻辑
        if (!hasPressedEnter && Input.GetKeyDown(KeyCode.Return))
        {
            hasPressedEnter = true;
            LockCursor();
        }

        // 只有按下了Enter键并且鼠标锁定后，才允许摄像头旋转
        if (hasPressedEnter && isCursorLocked && !Input.GetMouseButton(0))
        {
            // 获取鼠标在X和Y轴的移动
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 调整摄像头的垂直旋转
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 限制垂直旋转范围

            // 应用旋转到摄像头
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX); // 水平旋转角色
        }
    }

    public void ButtonLock(){
        
        // 如果按下了Enter键，启用鼠标锁定逻辑
        if (!hasPressedEnter )
        {
            hasPressedEnter = true;
            LockCursor();
        }
        
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true; // 更新锁定状态
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false; // 更新锁定状态
    }
}