using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelMovement : MonoBehaviour {

    public float speed = 0f;

    // Use this for initialization
    void Start () {

        StartCoroutine(RotateLeft(Vector3.back * 15f, 0.25f));
        StartCoroutine(RotateRight(Vector3.up * 25f, 0.25f));
        StartCoroutine(RotateLeft2(Vector3.down * 25f, 0.10f));
    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    IEnumerator RotateLeft(Vector3 byAngles, float inTime)
    {
        yield return new WaitForSeconds(34);

        speed = 350;

        yield return null;
    }

    IEnumerator RotateRight(Vector3 byAngles, float inTime)
    {
        yield return new WaitForSeconds(35);

        speed = -400;

        yield return null;
    }

    IEnumerator RotateLeft2(Vector3 byAngles, float inTime)
    {
        yield return new WaitForSeconds(36);

        speed = 550;

        yield return null;
    }

}
