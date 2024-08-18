using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;             // 门打开的角度
    public float openSpeed = 2f;              // 门打开的速度
    public bool isOpen = false;               // 追踪门是否打开或关闭的状态
    public float interactionDistance = 3f;    // 玩家可以与门互动的距离
    public Transform player;                  // 引用玩家的Transform

    private Quaternion closedRotation;        // 门的初始旋转（关闭位置）
    private Quaternion openRotation;          // 门的目标旋转（打开位置）

    void Start()
    {
        closedRotation = transform.rotation;  // 将初始旋转存储为关闭旋转
        openRotation = Quaternion.Euler(transform.eulerAngles + Vector3.up * openAngle);  // 计算打开时的旋转
    }

    void Update()
    {
        // 检查玩家是否在互动距离内
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance)
        {
            // 如果玩家按下"E"键
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen; // 切换门的状态（打开/关闭）
            }
        }

        // 平滑地旋转门到目标旋转角度
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}
