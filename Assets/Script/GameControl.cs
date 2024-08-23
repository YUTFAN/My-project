//using System.Collections;
//using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControll : MonoBehaviour
{

    private float deltaTime = 0.0f;
    private float logInterval = 1.0f; // Time between logs in seconds
    private float timeSinceLastLog = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        // Render under 120fps
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        //ShowFPS();

        // If "esc" pressed return to menu
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetKeyDown(KeyCode.Escape) && currentScene != 1) {
            Cursor.lockState = CursorLockMode.None;
            OpenMenu();
        }
    }

    // Showing FPS in log
    void ShowFPS(){
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        timeSinceLastLog += Time.unscaledDeltaTime;

        if (timeSinceLastLog >= logInterval)
        {
            Debug.Log($"FPS: {Mathf.Ceil(fps)}");
            timeSinceLastLog = 0.0f;
        }
    }

    public void GameStart(){
        SceneManager.LoadSceneAsync(0);
    }

    public void OpenMenu() {
        SceneManager.LoadSceneAsync(1);
    }
}






