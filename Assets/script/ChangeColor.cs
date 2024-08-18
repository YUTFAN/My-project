using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material[] materials; // 材质数组
    public GameObject uiPanel; // UI面板

    private Renderer _renderer;
    private int _currentMaterialIndex = 0;
    private bool canChangeColor = true; // 控制是否允许颜色改变

    void Start()
    {
        Application.targetFrameRate = 30;
        _renderer = GetComponent<Renderer>();
        
        // 确保Renderer和材料数组不为空
        if (_renderer != null && materials.Length > 0)
        {
            // 设置初始材质
            _renderer.material = materials[_currentMaterialIndex];
        }
        
        // 显示UI
        uiPanel.SetActive(true);
    }

    void Update()
    {
        // 如果允许颜色改变且按下空格键，就改变颜色
        if (canChangeColor && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("切换颜色");
            // 切换到下一个材质
            _currentMaterialIndex = (_currentMaterialIndex + 1) % materials.Length;
            _renderer.material = materials[_currentMaterialIndex];
        }

        // 检测Enter键是否按下
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 关闭UI
            uiPanel.SetActive(false);
            // 禁止进一步改变颜色
            canChangeColor = false;
        }
    }

    public void SwitchColor()
    {
        // 检查是否允许切换颜色
        if (canChangeColor)
        {
            Debug.Log("切换颜色");
            // 切换到下一个材质
            _currentMaterialIndex = (_currentMaterialIndex + 1) % materials.Length;
            _renderer.material = materials[_currentMaterialIndex];
        }
        
    }
    public void CloseUI(){
        // 关闭UI
        uiPanel.SetActive(false);
        // 禁止进一步改变颜色
        canChangeColor = false;
    }
}