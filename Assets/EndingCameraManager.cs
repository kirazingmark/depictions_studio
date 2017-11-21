using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCameraManager : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    public Camera Camera4;
    public Camera Camera5;
    public Camera Camera6;
    public GameObject WavingSophie;

    // Use this for initialization
    void Start()
    {

        Time.timeScale = 0.75f;

        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;
        Camera4.enabled = false;
        Camera5.enabled = false;
        Camera6.enabled = false;

        Cursor.visible = false;

        StartCoroutine(Camera1ToCamera2());
        StartCoroutine(Camera2ToCamera3());
        StartCoroutine(Camera3ToCamera4());
        StartCoroutine(Camera4ToCamera5());
        StartCoroutine(Camera5ToCamera6());
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
        yield return new WaitForSeconds(15.5f);
        Camera3.enabled = true;
        Camera2.enabled = false;
        yield return null;
    }

    IEnumerator Camera3ToCamera4()
    {
        yield return new WaitForSeconds(20);
        Camera4.enabled = true;
        Camera3.enabled = false;
        yield return null;
    }

    IEnumerator Camera4ToCamera5()
    {
        yield return new WaitForSeconds(30);
        Camera5.enabled = true;
        Camera4.enabled = false;
        yield return null;
    }

    IEnumerator Camera5ToCamera6()
    {
        yield return new WaitForSeconds(45);
        Camera6.enabled = true;
        Camera5.enabled = false;
        WavingSophie.SetActive(false);
        yield return null;
    }
}
