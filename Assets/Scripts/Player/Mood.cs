using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Mood : MonoBehaviour {

    public float determine = 1f; //changing how fast the meter can go down, the larger the slower. Was previously set to '1.0f'.

    public float happyMeter;
    public float temp;
    public bool painting;
    public float blinking;

    public FirstPersonController chara;
    public PickUpObject po;
    // Use this for initialization
    void Start () {

        happyMeter = 100;
        painting = false;

        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        po = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpObject>();

    }
	
	// Update is called once per frame
	void Update () {

        blinking += Time.deltaTime;

        if (/*happyMeter >= 0 ||*/ po.reading == true)
        {
           
            chara.m_WalkSpeed = 0;
        }
        else 
        {
            happyMeter -= Time.deltaTime / (determine * 3.0f);
        }

        // This needs to be fixed, Happiness can still rise above 100.
        if (happyMeter > 100)
        {
            happyMeter = 100f;
        }

        if ((int)happyMeter % 10 == 0)
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
   
        if(chara.m_WalkSpeed > 0)
        Camera.main.GetComponent<CameraEffect>().Fade = (100 - happyMeter) / 100;
        if (happyMeter <= 20)
        {
            Camera.main.GetComponent<CameraEffect>().rate = Mathf.Clamp(Mathf.Abs(Mathf.Sin(blinking) * 0.35f + 0.65f), 0.3f, 1.0f);
        }
    }
}
