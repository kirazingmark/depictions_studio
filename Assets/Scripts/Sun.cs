using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public float runSpeed;
    public GameObject time;
    public Hour hour;
    public float offset;
	// Use this for initialization
	void Start () {
        hour = time.GetComponent<Hour>();
	}
	
	// Update is called once per frame
	void Update () {
        float angle = (hour.accumulated * 0.00416666f+offset) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f) * 500.0f;
        transform.position = pos;
        //transform.RotateAround(Vector3.zero, Vector3.right, 0.6f*Time.deltaTime);
        transform.LookAt(Vector3.zero);

	}
}
