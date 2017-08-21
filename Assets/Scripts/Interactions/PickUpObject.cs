﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpObject : MonoBehaviour {

    public Camera mainCamera;
    public GameObject carriedObject;
    public bool isCarrying;
    public bool enter;
    public float distance;
    public float smooth;
    public float timer = 5;

    public AudioClip pickup;
    public AudioClip drop;
    public AudioClip teleportFire;
    public AudioClip teleportFireBack;
    public AudioSource audioPlayBack;

    public AudioSource toiletSource;
    public AudioClip toiletClip;

    public AudioSource mowerSource;
    public AudioClip mowerClip;

    public Font customFont;

    public string objectName;
    public bool sitting;

    public bool oneDay = true;
    bool onToilet;

    public Mood mood;
    CameraSwitcher pCamera;
    public FirstPersonController chara;

    public bool isMowerActive = false;
    public GameObject player;

    // Use this for initialization
    void Start () {

        audioPlayBack = GetComponent<AudioSource>();
        mood = GetComponent<Mood>();
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (isCarrying)
        {
            objectName = "";
            carry(carriedObject);
            checkDrop();
        }
        else
        {
            PickUp();
            TrackObjectName();
            ChangeCamera();
        }
    }

    void carry(GameObject objects)
    {
        objects.transform.position = Vector3.Lerp(objects.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
    }

    void PickUp()
    {
		if(Input.GetKeyDown("e"))
        {
            Debug.Log("tapped");
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 2))
            {

                PickUpable p = hit.collider.GetComponent<PickUpable>();

                if (p != null)
                {
                    audioPlayBack.PlayOneShot(pickup, 1.0F);
                    isCarrying = true;
                    carriedObject = p.gameObject;
                    p.rb.isKinematic = true;
                    mood.happyMeter += (int)p.point;
                    p.pickedUp = true;
                }

                Easel e = hit.collider.GetComponent<Easel>();
                {
                    if(e!= null)
                    {
                        pCamera.Camera7.enabled = true;
                    }
                }
            }
        }
    }

    void checkDrop()
    {
		if(Input.GetKeyDown(KeyCode.E))
        {
            dropObject();
        }
    }

    void dropObject()
    {
        audioPlayBack.PlayOneShot(drop, 1.0F);
        isCarrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }

    void TrackObjectName()
    {
        if(pCamera.Camera1.enabled == false)
        {

        }
        else
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                
                PickUpable p = hit.collider.GetComponent<PickUpable>();
                Easel e = hit.collider.GetComponent<Easel>();
                Chair c = hit.collider.GetComponent<Chair>();
                if (p != null)
                {
                    if (p.tag == "Food")
                    {
                        enter = true;
                        objectName = "<color=white><size=40>Consume - 'E'</size></color>";
                    }
                    else if (p.tag == "Trash")
                    {
                        enter = true;
                        objectName = "<color=white><size=40>Pick Up - 'E'</size></color>";
                    }
                    else if (p.tag == "Clothes")
                    {
                        enter = true;
                        objectName = "<color=white><size=40>Pick Up - 'E'</size></color>";
                    }
                    else
                    {

                    }
                }
                else if (e != null)
                {
                    enter = true;
                    objectName = "<color=white><size=40>Paint - 'E'</size></color>";

                }
                else if (c != null && c.tag != "LawnMower")
                {
                    enter = true;
                    objectName = "<color=white><size=40>Sit Down - 'E'</size></color>";
                    
                }
                else if (c != null && c.tag == "LawnMower")
                {
                    enter = true;
                    objectName = "<color=white><size=40>Ride Mower - 'E'</size></color>";

                }
                else
                    enter = false;
            }
        }
        
    }

    void OnGUI()
    {
        GUI.skin.font = customFont;

        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 350, 80), objectName);
        }
    }

    void ChangeCamera()
    {
        
        //sitting down
        if (Input.GetKeyDown("e") && !sitting)
        {
            pCamera.Camera1.enabled = false;
            if(pCamera.Camera1.enabled == false)
            {
                sitting = true;
                chara.m_WalkSpeed = 0;
            }
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                Chair c = hit.collider.GetComponent<Chair>();
                if (c != null)
                {
                    
                    if (c.tag == "chair")
                    {
                        pCamera.Camera2.enabled = true;
                        player.transform.position = new Vector3(-100, -100, -100);

                        if (pCamera.Camera2.enabled == true)
                        {
                            sitting = true;
                            chara.m_WalkSpeed = 0;
                        }
                    }
                    else if (c.tag == "chair2")
                    {
                        pCamera.Camera5.enabled = true;
                        player.transform.position = new Vector3(-100, -100, -100);

                        if (pCamera.Camera5.enabled == true)
                        {
                            sitting = true;
                            chara.m_WalkSpeed = 0;
                        }
                    }
                    
                    else if (c.tag == "chair3")
                    {
                        pCamera.Camera6.enabled = true;
                        player.transform.position = new Vector3(-100, -100, -100);

                        if (pCamera.Camera6.enabled == true)
                        {
                            sitting = true;
                            chara.m_WalkSpeed = 0;
                        }
                    }
                    else if (c.tag == "toilet")
                    {
                        if (!oneDay)
                            mood.happyMeter += 2;
                        pCamera.Camera4.enabled = true;
                        player.transform.position = new Vector3(-100, -100, -100);

                        if (pCamera.Camera4.enabled == true)
                        {
                            sitting = true;
                            chara.m_WalkSpeed = 0;
                            oneDay = true;
                        }
                    }
                    else if (c.tag == "toilet2")
                    {
                        if (!oneDay)
                            mood.happyMeter += 2;
                        pCamera.Camera3.enabled = true;
                        player.transform.position = new Vector3(-100, -100, -100);

                        if (pCamera.Camera3.enabled == true)
                        {
                            sitting = true;
                            chara.m_WalkSpeed = 0;
                            oneDay = true;
                        }
                    }
                    else if (c.tag == "LawnMower")
                    {
                        if (!oneDay)
                        mood.happyMeter += 0; // Mood will come from Grass Cut, not just sitting on the Mower.
                        pCamera.Camera8.enabled = true;
                        player.transform.position = new Vector3(-100, -100, -100);

                        if (pCamera.Camera8.enabled == true)
                        {
                            Debug.Log("On Mower is true");
                            isMowerActive = true;
                            sitting = true;
                            chara.m_WalkSpeed = 0;
                            oneDay = true;
                            mowerSource.Play();
                        }
                    }

                }
            }
           
        }
        else if(Input.GetKeyDown("e") && sitting)
        {
            if (pCamera.Camera3.enabled == true)
            {
                Debug.Log("Bathroom Camera Triggered.");
                toiletSource.Play();
                mood.happyMeter += 2; // Happens everytime, needs to be set to only once, or run off a cooldown timer.
                player.transform.position = new Vector3(-48, 2, 12); // Need to set.
                pCamera.Camera3.enabled = false;
            }
            else if (pCamera.Camera4.enabled == true)
            {
                Debug.Log("Bathroom Camera Triggered.");
                toiletSource.Play();
                mood.happyMeter += 2; // Happens everytime, needs to be set to only once, or run off a cooldown timer.
                player.transform.position = new Vector3(-48, 2, 4); // Need to set.
                pCamera.Camera4.enabled = false;
            }
            else if (pCamera.Camera8.enabled == true)
            {
                // Sound cues go here.
                Debug.Log("On Mower is false");
                isMowerActive = false;
                //player.transform.Translate(1, 1, 1);
                player.transform.position = new Vector3(-33, 2, 2);
                pCamera.Camera8.enabled = false;
                mowerSource.Stop();
            }
            else if (pCamera.Camera5.enabled == true)
            {
                player.transform.position = new Vector3(-46, 2, 10);
                pCamera.Camera5.enabled = false;
            }
            else if (pCamera.Camera2.enabled == true)
            {
                player.transform.position = new Vector3(-39, 2, -3);
                pCamera.Camera2.enabled = false;
            }
            else if (pCamera.Camera6.enabled == true)
            {
                player.transform.position = new Vector3(-41, 2, 2);
                pCamera.Camera6.enabled = false;
            }
            else
            {
                //objectName = "<color=white><size=40>Stand Up - 'F'</size></color>";

                sitting = false;

                //chara.m_WalkSpeed = 1 + (mood.happyMeter / 100);
            }
        }  
    }
}

