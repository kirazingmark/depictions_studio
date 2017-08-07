using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletCamera : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public float rotateSpeed = 2;
    
	// Use this for initialization
	void Start () {
		this.gameObject.transform.Rotate(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        float hozTurn = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime * 20;
        



        //applying the turning
        this.gameObject.transform.Rotate(0, hozTurn, 0);
        
    }


    
}
