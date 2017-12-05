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
            Cursor.visible = true;
            chara.m_MouseLook.lockCursor = false;
            chara.m_MouseLook.m_cursorIsLocked = false;
        }
        else if(pCamera.Camera1.enabled == true && Time.timeScale == 1)
        {
            Cursor.visible = false;
            chara.m_MouseLook.lockCursor = true;
            chara.m_MouseLook.m_cursorIsLocked = true;
        }

    }
}
