using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     

    void OnMouseDown()
    {
        if(gameObject.name == "eraser")
        {
            paintGM.toolType = "eraser";
        }

        if (gameObject.name == "pencil")
        {
            paintGM.toolType = "pencil";
        }

        if (gameObject.name == "sizeup")
        {
            paintGM.currentScale += 0.02f;
        }

        
    }
}
