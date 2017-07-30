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

    Mood playerMood;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        playerMood = GetComponent<Mood>();
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

    //function to destroy the garbage - put it outside the update function

    void OnCollisionEnter(Collider target)
    {
        if (target.gameObject.tag.Equals("bin") == true)
        {
            Destroy(this.gameObject);
            playerMood.happyMeter += 2;
        }
    }

}
