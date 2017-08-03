using System.Collections;
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
        //InvokeRepeating("PlayerControl", 10f, 10f);

        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (happyMeter >= 0)
        {
            happyMeter -= Time.deltaTime;
            temp += 1;
        }

        if(happyMeter % 10 == 0)
        {
            //Debug.Log("pie");
            chara.m_MouseLook.XSensitivity -= 0.01f;
            chara.m_MouseLook.YSensitivity -= 0.01f;
            if (chara.m_MouseLook.XSensitivity <= 1f)
                chara.m_MouseLook.XSensitivity = 1f;
            if (chara.m_MouseLook.YSensitivity <= 1f)
                chara.m_MouseLook.YSensitivity = 1f;
        }
   
        Camera.main.GetComponent<CameraEffect>().Fade = (100 - happyMeter) / 100;

    }

    //void PlayerControl()
    //{
    //    chara.m_MouseLook.XSensitivity -= 0.01f;
    //    chara.m_MouseLook.YSensitivity -= 0.01f;
    //    if (chara.m_MouseLook.XSensitivity <= 1f)
    //        chara.m_MouseLook.XSensitivity = 1f;
    //    if (chara.m_MouseLook.YSensitivity <= 1f)
    //        chara.m_MouseLook.YSensitivity = 1f;
    //}
}
