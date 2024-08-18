using UnityEngine;
using UnityEngine.Video;
using TMPro; // 导入TextMeshPro的命名空间

public class ScreenController : MonoBehaviour
{
    public VideoClip[] videoClips; // 存储多个视频
    public TextMeshProUGUI videoListText; // 使用TextMeshProUGUI替换Text
    public float activationDistance = 5f; // 激活屏幕的最大距离
    private VideoPlayer videoPlayer;
    private bool isScreenOn = false; // 跟踪屏幕是否开启
    private bool isPlaying = false; // 跟踪视频是否正在播放
    private int selectedVideoIndex = -1; // 当前选择的视频索引
    public Transform playerCamera; // 玩家摄像头

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false; // 禁用自动播放
        videoPlayer.Stop(); // 确保视频开始时不播放
        DisplayVideoList();
    }

    void Update()
    {
        // 检查按下 E 键，并且摄像头在正确的位置和方向
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtScreen())
        {
            if (isPlaying)
            {
                StopVideo(); // 如果视频正在播放，停止视频并返回视频选择界面
            }
            else if (isScreenOn)
            {
                TurnOffScreen(); // 如果屏幕已开启且未播放视频，则关闭屏幕
            }
            else
            {
                TurnOnScreen(); // 如果屏幕未开启，则打开屏幕
            }
        }

        // 显示视频名称列表并允许用户选择视频
        if (isScreenOn && !isPlaying)
        {
            HandleVideoSelection();
        }

        // 检查玩家是否按下鼠标左键暂停或继续播放
        if (Input.GetMouseButtonDown(0) && isPlaying)
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause(); // 如果视频正在播放，暂停
            }
            else
            {
                videoPlayer.Play(); // 如果视频已暂停，继续播放
            }
        }

        // 检查玩家是否按下左右箭头键并快进或快退
        if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SeekVideo(1); // 快进1秒
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SeekVideo(-1); // 快退1秒
            }
        }
    }

    private void DisplayVideoList()
    {
        videoListText.text = "Select one:\n";
        for (int i = 0; i < videoClips.Length; i++)
        {
            videoListText.text += (i + 1) + ". " + videoClips[i].name + "\n";
        }
        videoListText.gameObject.SetActive(false); // 初始状态下隐藏视频列表
    }

    private void TurnOnScreen()
    {
        isScreenOn = true;
        videoListText.gameObject.SetActive(true); // 显示视频列表
        Cursor.lockState = CursorLockMode.None; // 解锁鼠标
        Cursor.visible = true; // 显示鼠标
    }

    private void TurnOffScreen()
    {
        isScreenOn = false;
        videoListText.gameObject.SetActive(false); // 隐藏视频列表
        Cursor.lockState = CursorLockMode.Locked; // 重新锁定鼠标
        Cursor.visible = false; // 隐藏鼠标
    }

    // 检查摄像头是否在看着屏幕，并且距离在允许范围内
    private bool IsPlayerLookingAtScreen()
    {
        Vector3 directionToScreen = transform.position - playerCamera.position;
        float distanceToScreen = directionToScreen.magnitude;

        // 如果距离太远，返回false
        if (distanceToScreen > activationDistance)
            return false;

        directionToScreen.Normalize(); // 归一化方向向量

        // 检查摄像头的前向是否指向屏幕
        float dotProduct = Vector3.Dot(playerCamera.forward, directionToScreen);
        return dotProduct > 0.8f; // 0.8 表示约36度的视角，你可以调整这个值
    }

    private void HandleVideoSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标点击的位置
            Vector2 mousePosition = Input.mousePosition;
            // 将鼠标位置转换为UI坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(videoListText.rectTransform, mousePosition, null, out Vector2 localPoint);

            // 检查点击了哪一行
            for (int i = 0; i < videoClips.Length; i++)
            {
                // 计算每行的高度
                float lineHeight = videoListText.fontSize * 1.2f;
                // 计算每行的Y坐标
                float linePositionY = videoListText.rectTransform.rect.height / 2 - lineHeight * (i + 1);

                // 检查鼠标点击位置是否在这一行的区域内
                if (localPoint.y > linePositionY - lineHeight && localPoint.y < linePositionY)
                {
                    selectedVideoIndex = i;
                    PlayVideo(); // 播放选择的视频
                    return;
                }
            }
        }
    }

    private void PlayVideo()
    {
        if (selectedVideoIndex >= 0)
        {
            videoListText.gameObject.SetActive(false); // 隐藏视频列表
            videoPlayer.clip = videoClips[selectedVideoIndex]; // 设置选定的视频
            videoPlayer.Play(); // 播放视频
            isPlaying = true;
            Cursor.lockState = CursorLockMode.Locked; // 播放视频时重新锁定鼠标
            Cursor.visible = false; // 隐藏鼠标
        }
    }

    private void StopVideo()
    {
        videoPlayer.Stop();
        isPlaying = false;
        videoListText.gameObject.SetActive(true); // 显示视频列表
        Cursor.lockState = CursorLockMode.None; // 停止视频后解锁鼠标
        Cursor.visible = true; // 显示鼠标
    }

    private void SeekVideo(double seconds)
    {
        double newTime = videoPlayer.time + seconds;
        if (newTime < 0) newTime = 0; // 确保时间不为负数
        if (newTime > videoPlayer.length) newTime = videoPlayer.length; // 确保时间不超过视频长度
        videoPlayer.time = newTime;
    }
}
