using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour {

    // CONSTANTS AND VARIABLES.
    public float rotation = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            transform.Rotate(0, rotation * Time.deltaTime, 0);
    }
}
