using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarVeerRight : MonoBehaviour {

    public float spinSpeed = 0f;
    public float tipSpeed = 0f;

    // Use this for initialization
    void Start () {

        StartCoroutine(RotateLeft(Vector3.down * 25f, 1f));
        StartCoroutine(RotateRight(Vector3.up * 47.5f, 0.5f));
        //StartCoroutine(RotateLeft2(Vector3.down * 179f, 1.00f));
        // StartCoroutine(RotateLeft3(Vector3.down * 179f, 1.00f));
        StartCoroutine(CarSpin());
    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, tipSpeed * Time.deltaTime);
    }

    IEnumerator RotateLeft(Vector3 byAngles, float inTime)
    {
        yield return new WaitForSeconds(34);

        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    IEnumerator RotateRight(Vector3 byAngles, float inTime)
    {
        yield return new WaitForSeconds(35);

        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    //IEnumerator RotateLeft2(Vector3 byAngles, float inTime)
    //{
    //    yield return new WaitForSeconds(13);

    //    Quaternion fromAngle = transform.rotation;
    //    Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
    //    for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
    //    {
    //        transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
    //        yield return null;
    //    }
    //}

    //IEnumerator RotateLeft3(Vector3 byAngles, float inTime)
    //{
    //    yield return new WaitForSeconds(14);

    //    Quaternion fromAngle = transform.rotation;
    //    Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
    //    for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
    //    {
    //        transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
    //        yield return null;
    //    }
    //}

    IEnumerator CarSpin()
    {
        yield return new WaitForSeconds(36);

        spinSpeed = 450.0f;

        yield return null;
    }
}