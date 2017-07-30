using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class Mood : MonoBehaviour {

    public float determine = 1.0f; //changing how fast the meter can go down, the larger the slower;

    public float happyMeter;

    public FirstPersonController fps;
    // Use this for initialization
    void Start () {

        happyMeter = 100;

        fps = GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {

        happyMeter -= Time.deltaTime;
        if(happyMeter <= 0)
        {
            happyMeter = 0;
        }

        if(happyMeter % 10 == 0)
        {
          
        }

        Camera.main.GetComponent<CameraEffect>().Fade = (100 - happyMeter) / 100;

    }
}
