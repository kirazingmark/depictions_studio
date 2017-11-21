using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveBackwards : MonoBehaviour {

    public float movementSpeed = 10;
    public bool cameraActive = false;

    // Use this for initialization
    void Start () {

        StartCoroutine(Camera6Activate());
	}
	
	// Update is called once per frame
	void Update () {

        if (cameraActive == true)
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }
    }

    IEnumerator Camera6Activate()
    {
        yield return new WaitForSeconds(45);
        cameraActive = true;
        yield return null;
    }
}
