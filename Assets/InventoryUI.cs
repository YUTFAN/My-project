using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // 引用 Inventory 类
    public List<Image> itemSlots; // UI 图像列表，与物品栏槽位对应
    public Sprite defaultSprite;  // 空槽位时显示的默认图标

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < inventory.items.Count)
            {
                // 获取物品的图标并显示在 UI 槽位中
                itemSlots[i].sprite = inventory.items[i].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                // 显示默认图标
                itemSlots[i].sprite = defaultSprite;
            }
        }
    }
}
