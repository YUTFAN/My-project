using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float pickupRange = 2f; // 拾取距离
    public Transform playerCamera; // 玩家摄像机的Transform
    private GameObject heldObject = null; // 当前持有的物体
    private GameObject targetedObject = null; // 当前瞄准的物体
    private Color originalColor; // 球体原来的颜色
    public float scaleSpeed = 0.1f; // 缩放速度

    void Update()
    {
        // 如果玩家按下E键并且当前没有持有物体
        if (Input.GetKeyDown(KeyCode.E) && heldObject == null && targetedObject != null)
        {
            Pickup(targetedObject); // 拾取该物体
        }
        // 如果玩家按下鼠标右键并且当前持有物体
        else if (Input.GetMouseButtonDown(1) && heldObject != null)
        {
            DropObject(); // 放下物体
        }

        // 如果持有物体，处理物体的旋转和缩放
        if (heldObject != null)
        {
            // 仅当按住鼠标左键时旋转物体
            if (Input.GetMouseButton(0))
            {
                RotateHeldObject(); // 旋转持有的物体
            }

            // 使用鼠标滚轮缩放物体
            ScaleHeldObject(); // 缩放持有的物体
        }
        else
        {
            HighlightObject(); // 高亮显示可拾取的物体
        }
    }

    void HighlightObject()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // 如果射线命中物体并且该物体有Pickup标签
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.gameObject.CompareTag("Pickup"))
            {
                if (targetedObject != hit.collider.gameObject)
                {
                    // 恢复之前目标物体的颜色
                    if (targetedObject != null)
                    {
                        Renderer prevRenderer = targetedObject.GetComponent<Renderer>();
                        prevRenderer.material.color = originalColor;
                    }

                    // 记录新的目标物体和它的颜色
                    targetedObject = hit.collider.gameObject;
                    Renderer renderer = targetedObject.GetComponent<Renderer>();
                    originalColor = renderer.material.color;

                    // 将物体颜色改为蓝色
                    renderer.material.color = Color.blue;
                }
                return;
            }
        }

        // 如果没有命中可拾取物体，恢复原颜色
        if (targetedObject != null)
        {
            Renderer renderer = targetedObject.GetComponent<Renderer>();
            renderer.material.color = originalColor;
            targetedObject = null;
        }
    }

    void Pickup(GameObject obj)
    {
        // 在拾取时恢复物体的原始颜色
        Renderer renderer = obj.GetComponent<Renderer>();
        renderer.material.color = originalColor;

        heldObject = obj;
        heldObject.GetComponent<Rigidbody>().isKinematic = true; // 禁用物理模拟
        heldObject.transform.SetParent(playerCamera); // 将物体设置为摄像机的子对象

        // 调整物体离摄像头的距离，这里设定为前方2个单位
        heldObject.transform.localPosition = new Vector3(0, 0, 2); 
    }

    void DropObject()
    {
        heldObject.GetComponent<Rigidbody>().isKinematic = false; // 启用物理模拟
        heldObject.transform.SetParent(null); // 解除父子关系
        heldObject = null;
    }

    void RotateHeldObject()
    {
        float rotateX = Input.GetAxis("Mouse X") * Time.deltaTime * 250f;
        float rotateY = Input.GetAxis("Mouse Y") * Time.deltaTime * 250f;

        heldObject.transform.Rotate(playerCamera.up, -rotateX, Space.World);
        heldObject.transform.Rotate(playerCamera.right, rotateY, Space.World);
    }

    void ScaleHeldObject()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // 基于鼠标滚轮输入缩放物体
            Vector3 newScale = heldObject.transform.localScale + Vector3.one * scrollInput * scaleSpeed;
            
            // 防止物体缩放到负值或过小
            newScale = new Vector3(
                Mathf.Max(newScale.x, 0.1f),
                Mathf.Max(newScale.y, 0.1f),
                Mathf.Max(newScale.z, 0.1f)
            );

            heldObject.transform.localScale = newScale;
        }
    }
}
