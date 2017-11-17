using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour
{

    public enum ObjectType { Bad, Good, VeryGood, Alcohol, Trash, Clothes, Grass };
    public enum ObjectType2 { Food, Stuffs }

    public ObjectType ot;
    public ObjectType2 ot2;

    public Rigidbody rb;
    public bool pickedUp = false;
    public float point;

    public Mood playerMood;
    public PickUpObject po;

    // Use this for initialization
    void Start()
    {
        po = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpObject>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        playerMood = GameObject.FindGameObjectWithTag("Player").GetComponent<Mood>();
        if (ot == ObjectType.Bad)
            point = -5f;
        else if (ot == ObjectType.Alcohol)
            point = -10f;
        else if (ot == ObjectType.Good)
            point = 5f;
        else if (ot == ObjectType.VeryGood)
            point = 50f;
        else if (ot == ObjectType.Trash)
            point = 0f;
        else if (ot == ObjectType.Clothes)
            point = 0f;
        else if (ot == ObjectType.Grass)
            point = 0f;
        else
            point = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (pickedUp)
        {
            if (ot2 == ObjectType2.Food)
            {
                Debug.Log("food");
                Destroy(this.gameObject);
                po.isCarrying = false;
                po.carriedObject = null;
            }
            else
            {

            }
        }
    }

    //function to destroy the garbage - put it outside the update function

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "bin" && ot != ObjectType.Clothes)
        {
            Debug.Log("hitting");
            Destroy(this.gameObject);
            playerMood.happyMeter += 2;
            po.carriedObject = null;
            po.isCarrying = false;
        }
        else if (target.gameObject.tag == "clothesHamper" && ot == ObjectType.Clothes)
        {
            Debug.Log("hitting");
            Destroy(this.gameObject);
            playerMood.happyMeter += 2;
            po.carriedObject = null;
            po.isCarrying = false;
        }
        else if (target.gameObject.tag == "MowerDestroyer" && ot == ObjectType.Grass)
        {
            Debug.Log("grass cut");
            Destroy(this.gameObject);
            playerMood.happyMeter += 0.15f; // May need to be tweaked.
            po.carriedObject = null;
            po.isCarrying = false;
        }
        else
        {

        }
    }
}
