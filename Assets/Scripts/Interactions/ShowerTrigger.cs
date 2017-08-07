using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerTrigger : MonoBehaviour {

    // CONSTANTS & VARIABLES.

    public ParticleSystem Water;
    public ParticleSystem Mist;
    public ParticleSystem Splashes;
    public bool isActive;
    public bool enter;

    public AudioSource showerSource;
    public AudioClip showerTrack;

    public Font customFont;

    public Mood mood;

    // Use this for initialization
    void Start () {

        mood = GetComponent<Mood>();
        Water = GetComponent<ParticleSystem>();
        Mist = GetComponent<ParticleSystem>();
        Splashes = GetComponent<ParticleSystem>();
        isActive = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("E") && enter && !isActive)
        {
            StartShower();
        }
        else if (Input.GetButtonDown("E") && enter && isActive)
        {
            StopShower();
            //mood.happyMeter += 5; // Not currently working, throws a NullReferenceException.
        }
        else if (Input.GetButtonDown("E") && !enter && isActive)
        {
            // Nothing.
        }
        else
        {
            // Nothing.
        }
    }

    void StartShower()
    {
        showerSource.Play();
        Water.Play();
        Water.enableEmission = true;
        Mist.Play();
        Mist.enableEmission = true;
        Splashes.Play();
        Splashes.enableEmission = true;
        isActive = true;
    }

    void StopShower()
    {
        showerSource.Stop();
        Water.Stop();
        Water.enableEmission = false;
        Mist.Stop();
        Mist.enableEmission = false;
        Splashes.Stop();
        Splashes.enableEmission = false;
        isActive = false;
    }

    //Activate the Main function when player is in Shower.
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }

    //Deactivate the Main function when player is out of Shower.
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }

    void OnGUI()
    {

        GUI.skin.font = customFont;

        if (enter)
        {

            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 350, 80), "<color=white><size=40>On/Off - 'E'</size></color>");
        }
    }
}
