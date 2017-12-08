using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CursorManager : MonoBehaviour {

    CameraSwitcher pCamera;
    public FirstPersonController chara;

    // Use this for initialization
    void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
    }
	
	// Update is called once per frame
	void Update () {

        if (pCamera.Camera7.enabled == true)
        {
            pCamera.Camera1.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (pCamera.Camera7.enabled == false && Time.timeScale == 1)
        {
            pCamera.Camera1.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
