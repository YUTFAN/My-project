using UnityEngine;

public class WhiteboardInteraction : MonoBehaviour
{
    public float interactionDistance = 3.0f;  // 交互距离
    public Camera playerCamera;  // 玩家摄像机
    public RenderTexture whiteboardTexture;  // 白板上的绘画纹理
    public Texture2D brushTexture;  // 画笔纹理
    public Color brushColor = Color.black;  // 画笔颜色
    private bool isDrawing = false;  // 用于跟踪玩家是否在绘画

    private Material brushMaterial;  // 用于绘画的材质

    private void Start()
    {
        // 初始化材质，并设置画笔颜色
        brushMaterial = new Material(Shader.Find("Unlit/Texture"));
        brushMaterial.SetColor("_Color", brushColor);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerCloseEnough())
        {
            ToggleDrawingMode();
        }

        if (isDrawing)
        {
            DrawOnWhiteboard();
        }
    }

    private bool IsPlayerCloseEnough()
    {
        float distance = Vector3.Distance(playerCamera.transform.position, transform.position);
        return distance <= interactionDistance;
    }

    private void ToggleDrawingMode()
    {
        isDrawing = !isDrawing;
        Cursor.lockState = isDrawing ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isDrawing;
    }

    private void DrawOnWhiteboard()
    {
        if (Input.GetMouseButton(0)) // 左键绘画
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    Vector2 uv;
                    uv.x = hit.textureCoord.x;
                    uv.y = hit.textureCoord.y;
                    DrawAt(uv);
                }
            }
        }
    }

    private void DrawAt(Vector2 uv)
    {
        RenderTexture.active = whiteboardTexture;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, whiteboardTexture.width, whiteboardTexture.height, 0);
        Graphics.DrawTexture(new Rect(uv.x * whiteboardTexture.width, (1 - uv.y) * whiteboardTexture.height, brushTexture.width, brushTexture.height), brushTexture, brushMaterial);
        GL.PopMatrix();
        RenderTexture.active = null;
    }
}