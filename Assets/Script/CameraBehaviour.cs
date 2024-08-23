// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Lock the cursor within the game window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
