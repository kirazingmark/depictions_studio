﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Mood : MonoBehaviour {

    public float determine = 1.0f; //changing how fast the meter can go down, the larger the slower;

    public float happyMeter;
    public float temp;

    public FirstPersonController chara;
    // Use this for initialization
    void Start () {

        happyMeter = 100;
        

        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (happyMeter >= 0)
        {
            happyMeter -= Time.deltaTime/determine;
           
        }

        if((int)happyMeter % 10 == 0)
        {
            //Debug.Log("pie");
            chara.m_MouseLook.XSensitivity = 1 + (happyMeter/100);
            chara.m_MouseLook.YSensitivity = 1 + (happyMeter / 100);
            chara.m_WalkSpeed = 1 + (happyMeter / 100);
            if (chara.m_MouseLook.XSensitivity <= 1f)
                chara.m_MouseLook.XSensitivity = 1f;
            if (chara.m_MouseLook.YSensitivity <= 1f)
                chara.m_MouseLook.YSensitivity = 1f;
        }
   
        Camera.main.GetComponent<CameraEffect>().Fade = (100 - happyMeter) / 100;

    }
}
