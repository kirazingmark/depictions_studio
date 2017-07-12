using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour {

    public Rigidbody rb;
    public bool on = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (on)
        //    this.rb.isKinematic = true;
        //else
        //    this.rb.isKinematic = false;
		
	}
}
