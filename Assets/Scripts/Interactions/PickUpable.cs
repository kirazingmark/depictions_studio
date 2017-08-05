using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour
{

    public enum ObjectType { Bad, Good, Alcohol, Trash, Clothes, Clock };
    public enum ObjectType2 { Food, Stuffs, AlarmClock }

    public ObjectType ot;
    public ObjectType2 ot2;

    public Rigidbody rb;
    public bool pickedUp = false;
    public float point;

    public Mood playerMood;
    public PickUpObject po;

    public AudioClip alarmClickSound;
    public AudioSource clickSource;
    public AudioClip alarmSound;
    public AudioSource alarmSource;
    

    //public AudioSource alarmSound;
    //public AudioSource alarmClick;
    //public AudioClip alarmClickSound;

    // Use this for initialization
    void Start()
    {

        alarmSource.loop = true;
        alarmSource.PlayOneShot(alarmSound, 1.0f);

        po = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpObject>();
        rb = GetComponent<Rigidbody>();



        playerMood = GameObject.FindGameObjectWithTag("Player").GetComponent<Mood>();
        if (ot == ObjectType.Bad)
            point = -5f;
        else if (ot == ObjectType.Alcohol)
            point = -10f;
        else if (ot == ObjectType.Good)
            point = 5f;
        else if (ot == ObjectType.Trash)
            point = 0f;
        else if (ot == ObjectType.Clothes)
            point = 0f;
        else if (ot == ObjectType.Clock)
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
            else if (ot2 == ObjectType2.AlarmClock)
            {
                Debug.Log("alarmclock");
                alarmSource.Stop();
                clickSource.PlayOneShot(alarmClickSound, 1.0F);
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
        if (target.gameObject.tag == "bin")
        {
            Debug.Log("hitting");
            Destroy(this.gameObject);
            playerMood.happyMeter += 2;
            po.carriedObject = null;
            po.isCarrying = false;
        }
        else if (target.gameObject.tag == "Clothes")
        {
            Debug.Log("hitting");
            Destroy(this.gameObject);
            playerMood.happyMeter += 2;
            po.carriedObject = null;
            po.isCarrying = false;
        }
    }
}
