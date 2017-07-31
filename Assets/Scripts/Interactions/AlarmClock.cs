using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    // VARIABLES AND CONSTANTS.
    public AudioSource alarmSound;
    public AudioSource alarmClick;
    public AudioClip alarmClickSound;
    private bool enter;
    public Rigidbody rb;

    private GUIStyle guiStyle = new GUIStyle();

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        alarmSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("E") && enter)
        {

            alarmSound.Stop();
            alarmClick.PlayOneShot(alarmClickSound, 1.0F);
        }

    }

    void OnGUI()
    {

        if (enter)
        {

            guiStyle.fontSize = 35; //change the font size
            guiStyle.fontStyle = FontStyle.Bold;
            guiStyle.font = (Font)Resources.Load("Fonts/bebas_neue/BebasNeue");
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 350, 80), "<color=white><size=35>Turn Off - 'E'</size></color>", guiStyle);
        }
    }

    //Activate the Main function when player is near the Door.
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }

    //Deactivate the Main function when player is go away from Door.
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }
}