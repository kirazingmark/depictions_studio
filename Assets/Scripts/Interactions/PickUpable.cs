using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour {

    public enum ObjectType { Bad, Good };

    public ObjectType ot;

    public Rigidbody rb;
    public float point;
    public bool on = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        if (ot == ObjectType.Bad)
            point = -5f;
        else
            point = 5f;
        
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}
}
