using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour {

    public enum ObjectType { Bad, Good, Alcohol };
    public enum ObjectType2 { Food , Stuffs}
    
    public ObjectType ot;
    public ObjectType2 ot2;

    public Rigidbody rb;
    public bool pickedUp = false;
    public float point;
    public bool on = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        if (ot == ObjectType.Bad)
            point = -5f;
        else if (ot == ObjectType.Alcohol)
            point = -10f;
        else
            point = 5f;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (pickedUp)
        {
            if (ot2 == ObjectType2.Food)
            {
                Destroy(this.gameObject);
            }
            else
            {

            }
        }
    }
}
