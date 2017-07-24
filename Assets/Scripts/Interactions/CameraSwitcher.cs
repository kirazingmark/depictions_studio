using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public Camera MainCamera;
    public Camera SofaCamera;
    private bool sofaActive;

    // Use this for initialization
    void Start () {

        MainCamera.enabled = true;
        SofaCamera.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (sofaActive)
        {

            // Sit Down.
            MainCamera.enabled = false;
            SofaCamera.enabled = true;
        }
        else
        {

            // Get Up.
            MainCamera.enabled = true;
            SofaCamera.enabled = false;
        }

        if (Input.GetButtonDown("E") && sofaActive)
        {
            sofaActive = !sofaActive;
        }
        else if (Input.GetButtonDown("E") && !sofaActive)
        {
            // Nothing.
        }
        else
        {
            // Nothing.
        }
    }

    void OnGUI()
    {

        if (sofaActive)
        {

            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 350, 80), "<color=white><size=35>Sit/Get Up - 'E'</size></color>");
        }
    }

    //Activate the Main function when player is near the Sofa.
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            sofaActive = true;
        }
    }

    //Deactivate the Main function when player is away from Sofa.
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            sofaActive = false;
        }
    }
}
