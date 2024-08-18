using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;   // 角色控制器组件
    public float speed = 5.0f;                // 移动速度
    public float jumpHeight = 2.0f;           // 跳跃高度
    public float gravity = -9.81f;            // 重力值
    public float groundCheckDistance = 0.1f;  // 地面检测距离

    private Vector3 velocity;                 // 速度向量
    private bool isGrounded;                  // 是否在地面上

    void Start()
    {
        controller = GetComponent<CharacterController>();  // 获取CharacterController组件
    }

    void Update()
    {
        // 检测角色是否在地面上
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // 如果在地面上，重置垂直速度（设置为-2f以确保稳定接触地面）
        }

        // 获取键盘输入
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 move = transform.right * x + transform.forward * z;

        // 控制器移动
        controller.Move(move * speed * Time.deltaTime);

        // 处理跳跃逻辑
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // 计算跳跃速度
        }

        //处理加速奔跑
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
        }
        else
        {
            speed = 5.0f;
        }
        // 应用重力
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);  // 移动控制器
    }
}
