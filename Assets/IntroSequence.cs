using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public Camera introCamera;  // 初始摄像头
    public Camera playerCamera;  // 玩家摄像头


    private bool introActive = true;

    void Start()
    {
        // 初始化设置：启用IntroCamera，禁用PlayerCamera
        introCamera.enabled = true;
        playerCamera.enabled = false;

    }

    void Update()
    {
        // 检测玩家是否按下回车键
        if (introActive && Input.GetKeyDown(KeyCode.Return))
        {
            EndIntro();
        }
    }

    public void EndIntro()
    {
        // 切换到玩家摄像头
        introCamera.enabled = false;
        playerCamera.enabled = true;

        // 标记介绍结束
        introActive = false;
    }
}
