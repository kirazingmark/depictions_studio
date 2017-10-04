using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlPaint : MonoBehaviour {

    CameraSwitcher pCamera;
    public Button exitButton;

	// Use this for initialization
	void Start () {
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
		
	}

    void Update()
    {
        if (pCamera.Camera7.enabled == true)
            exitButton.gameObject.SetActive(true);
        else
            exitButton.gameObject.SetActive(false);
    }

    public void exitPainting() //exiting the painting part
    {
        pCamera.Camera7.enabled = false;
    }
}
