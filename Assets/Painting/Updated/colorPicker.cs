using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ColorChange();
	}

    void ColorChange()
    {
        if (Input.GetKeyDown("r"))
        {
            paintGM.currentColor = Color.red;
        }
        if(Input.GetKeyDown("b"))
        {
            paintGM.currentColor = Color.blue;
        }
        if (Input.GetKeyDown("g"))
        {
            paintGM.currentColor = Color.green;
        }
        if(Input.GetKeyDown("v"))
        {
            paintGM.currentColor = Color.black;
        }
    }
}
