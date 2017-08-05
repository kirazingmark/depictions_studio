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

    public Font customFont;

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
        GUI.skin.font = customFont;

        if (enter)
        {

            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 350, 80), "<color=white><size=40>Turn Off - 'E'</size></color>");
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