using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    CameraSwitcher pCamera;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
    }
	
	// Update is called once per frame
	void Update () {

        if (pCamera.Camera7.enabled == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
		
	}
}
