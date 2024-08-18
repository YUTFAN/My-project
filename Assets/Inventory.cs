using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>(); // 存储物品的列表
    public int inventorySize = 5; // 物品栏大小

    public void AddItem(GameObject item)
    {
        if (items.Count < inventorySize)
        {
            items.Add(item);
            item.SetActive(false); // 隐藏物品
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public GameObject GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            GameObject item = items[index];
            items.RemoveAt(index);
            return item;
        }
        return null;
    }
}
