using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public Camera Camera1; // Main Camera on Player.
    public Camera Camera2; // Sofa Camera.
    public Camera Camera3; // EnSuite Toilet Camera.
    public Camera Camera4; // Main Toilet Camera.
    public Camera Camera5; // Desk Camera.
    public Camera Camera6; // Dining Table Camera.

    // Use this for initialization
    void Start()
    {

        Camera1.enabled = true; // Main Camera on Player enabled by default, all others disabled at runtime.
        Camera2.enabled = false;
        Camera3.enabled = false;
        Camera4.enabled = false;
        Camera5.enabled = false;
        Camera6.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Camera 1.
        if (Input.GetKeyDown(KeyCode.Alpha1) && (Camera2.enabled == true || Camera3.enabled == true || Camera4.enabled == true || Camera5.enabled == true || Camera6.enabled == true))
        {
            Camera1.enabled = true;
            Camera2.enabled = false;
            Camera3.enabled = false;
            Camera4.enabled = false;
            Camera5.enabled = false;
            Camera6.enabled = false;
        }
        // Camera 2.
        else if (Input.GetKeyDown(KeyCode.Alpha2) && (Camera1.enabled == true || Camera3.enabled == true || Camera4.enabled == true || Camera5.enabled == true || Camera6.enabled == true))
        {
            Camera1.enabled = false;
            Camera2.enabled = true;
            Camera3.enabled = false;
            Camera4.enabled = false;
            Camera5.enabled = false;
            Camera6.enabled = false;
        }
        // Camera 3.
        else if (Input.GetKeyDown(KeyCode.Alpha3) && (Camera1.enabled == true || Camera2.enabled == true || Camera4.enabled == true || Camera5.enabled == true || Camera6.enabled == true))
        {
            Camera1.enabled = false;
            Camera2.enabled = false;
            Camera3.enabled = true;
            Camera4.enabled = false;
            Camera5.enabled = false;
            Camera6.enabled = false;
        }
        // Camera 4.
        else if (Input.GetKeyDown(KeyCode.Alpha4) && (Camera1.enabled == true || Camera2.enabled == true || Camera3.enabled == true || Camera5.enabled == true || Camera6.enabled == true))
        {
            Camera1.enabled = false;
            Camera2.enabled = false;
            Camera3.enabled = false;
            Camera4.enabled = true;
            Camera5.enabled = false;
            Camera6.enabled = false;
        }
        // Camera 5.
        else if (Input.GetKeyDown(KeyCode.Alpha5) && (Camera1.enabled == true || Camera2.enabled == true || Camera3.enabled == true || Camera4.enabled == true || Camera6.enabled == true))
        {
            Camera1.enabled = false;
            Camera2.enabled = false;
            Camera3.enabled = false;
            Camera4.enabled = false;
            Camera5.enabled = true;
            Camera6.enabled = false;
        }
        // Camera 6.
        else if (Input.GetKeyDown(KeyCode.Alpha6) && (Camera1.enabled == true || Camera2.enabled == true || Camera3.enabled == true || Camera4.enabled == true || Camera5.enabled == true))
        {
            Camera1.enabled = false;
            Camera2.enabled = false;
            Camera3.enabled = false;
            Camera4.enabled = false;
            Camera5.enabled = false;
            Camera6.enabled = true;
        }
    }
}
