using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;

    // Use this for initialization
    void Start () {

        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;

        Cursor.visible = false;

        StartCoroutine(Camera1ToCamera2());
        StartCoroutine(Camera2ToCamera3());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Camera1ToCamera2()
    {
        yield return new WaitForSeconds(10);
        Camera2.enabled = true;
        Camera1.enabled = false;
        yield return null;
    }

    IEnumerator Camera2ToCamera3()
    {
        yield return new WaitForSeconds(37);
        Camera3.enabled = true;
        Camera2.enabled = false;
        yield return null;
    }

}
