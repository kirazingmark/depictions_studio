using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotControl : MonoBehaviour {

    paintGM paint;

	// Use this for initialization
	void Start () {
        GetComponent<Transform>().localScale = new Vector2(paintGM.currentScale, paintGM.currentScale);
        paint = GetComponent<paintGM>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(this.gameObject);
        }

	}

    private void OnMouseOver()
    {
        if(paintGM.toolType == "eraser")
        {
            Destroy(gameObject);
        }
    }
}
