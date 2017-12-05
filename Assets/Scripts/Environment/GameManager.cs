using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float determine = 1.0f; //changing how fast the meter can go down, the larger the slower;

    public float happyMeter;
    public float happyScore;
    public float sadScore;

    public Text level;
    public Text goodScore;
    public Text badScore;

    // Use this for initialization
    void Start () {
        happyMeter = 100.0f;
        happyScore = 10.0f;
        sadScore = 10.0f;
        //Camera.main.GetComponent<CameraEffect>().night = 1;
        
    }
	
	// Update is called once per frame
	void Update () {

        level.text = "Happiness Level: " + happyMeter.ToString("f0");
        goodScore.text = "Good Score: " + happyScore.ToString();
        badScore.text = "Bad Score: " + sadScore.ToString();

        happyMeter -= Time.deltaTime / determine;
        if (happyMeter <= 0)
            happyMeter = 0;

        //if (Input.GetKeyDown("f") || Input.GetMouseButtonDown(0))
        //{
        //    happyMeter += happyScore;
        //}
        //if(Input.GetKeyDown("r") || Input.GetMouseButtonDown(1))
        //{
        //    happyMeter -= sadScore;
        //}

      
        Camera.main.GetComponent<CameraEffect>().Fade = (100 - happyMeter) / 100;

    }
}
