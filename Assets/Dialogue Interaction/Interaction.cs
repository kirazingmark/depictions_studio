using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Ray ext = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ext, out hit, 10.0f))
            {
                GameObject target = hit.collider.gameObject;
                Debug.Log(target);
                target.GetComponent<Target>().PlayDialogue();
            }
        }
           
		
	}
}
