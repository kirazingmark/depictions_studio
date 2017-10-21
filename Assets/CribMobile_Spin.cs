﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CribMobile_Spin : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public float rotationSpeed = 5.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }
}
