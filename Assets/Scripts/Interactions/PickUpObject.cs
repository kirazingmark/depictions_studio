using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI; // For Notes UI Elements.
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpObject : MonoBehaviour {

    public Camera mainCamera;
    public GameObject carriedObject;
    public bool isCarrying;
    public bool enter;
    public float distance;
    public float smooth;
    public float timer = 5;

    // Note Sprite UI Elements.
    public GameObject ReadingPanel;
    public GameObject Ethan_Note1;
    public GameObject Ethan_Note2;
    public GameObject Ethan_Note3;
    public GameObject Ethan_Note4;
    public GameObject Ethan_Note5;
    public GameObject Sophie_Note1;
    public GameObject Sophie_Note2;
    public GameObject Sophie_Note3;

    // Note Door Lock GameObjects.
    public GameObject Note1_DoorLock;

    // Note AudioClip Elements.
    public AudioClip note_Ethan1;
    public AudioClip note_Ethan2;
    public AudioClip note_Ethan3;
    public AudioClip note_Ethan4;
    public AudioClip note_Ethan5;
    public AudioClip note_Sophie1;
    public AudioClip note_Sophie2;
    public AudioClip note_Sophie3;
    public AudioClip doorUnlock;

    // Note Check If Played Flags.
    public bool note_Ethan1_Played = false;
    public bool note_Ethan2_Played = false;
    public bool note_Ethan3_Played = false;
    public bool note_Ethan4_Played = false;
    public bool note_Ethan5_Played = false;
    public bool note_Sophie1_Played = false;
    public bool note_Sophie2_Played = false;
    public bool note_Sophie3_Played = false;

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

        // Note UI Elements being set to FALSE by default.
        ReadingPanel.SetActive(false);
        Ethan_Note1.SetActive(false);
        Ethan_Note2.SetActive(false);
        Ethan_Note3.SetActive(false);
        Ethan_Note4.SetActive(false);
        Ethan_Note5.SetActive(false);
        Sophie_Note1.SetActive(false);
        Sophie_Note2.SetActive(false);
        Sophie_Note3.SetActive(false);
        
        // Note Door Lock GameObjects being set to TRUE by default.
        Note1_DoorLock.SetActive(true);
        // REMAINING DOOR LOCKS TO BE ADDED HERE.
    }

    // MOVE FUNCTION
    public IEnumerator EthanNote1_Function()
    {
        Debug.Log("Ethan 1 Function Called!");

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Ethan_Note1.SetActive(true);

        audioPlayBack.clip = note_Ethan1;
        audioPlayBack.PlayOneShot(note_Ethan1, 1.0f);

        yield return new WaitForSeconds(audioPlayBack.clip.length);
        audioPlayBack.clip = doorUnlock;
        audioPlayBack.Play();

        // Disable Note 1 Door Lock GameObject.
        Note1_DoorLock.SetActive(false);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Ethan_Note1.SetActive(false);

        // Change Note 1 Flag.
        note_Ethan1_Played = true;
    }

    // Update is called once per frame
    void Update () {

        if (pCamera.Camera1.enabled == true)
            chara.m_WalkSpeed = 1 + (mood.happyMeter / 100);

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

                //Easel e = hit.collider.GetComponent<Easel>();
                //{
                //    if(e!= null)
                //    {
                //        pCamera.Camera7.enabled = true;
                //    }
                //}
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
            if (Physics.Raycast(ray, out hit))
            {
                
                PickUpable p = hit.collider.GetComponent<PickUpable>();
                Easel e = hit.collider.GetComponent<Easel>();
                Chair c = hit.collider.GetComponent<Chair>(); // Notes are also classed under Chairs in this script.
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
                        objectName = "";
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
                    objectName = "<color=white><size=40>Read - 'E'</size></color>"; // Temporarily set to read, current issue with case two statements down related to Ethan's Note.
                    
                }
                else if (c != null && c.tag == "LawnMower")
                {
                    enter = true;
                    objectName = "<color=white><size=40>Ride Mower - 'E'</size></color>";

                }

                // || c.tag == "ReadableNote_Ethan2" || c.tag == "ReadableNote_Ethan3" || c.tag == "ReadableNote_Ethan4" || c.tag == "ReadableNote_Ethan5" || c.tag == "ReadableNote_Sophie1" || c.tag == "ReadableNote_Sophie2" || c.tag == "ReadableNote_Sophie3"
                else if (c != null && c.tag == "ReadableNote_Ethan1")
                {
                    enter = true;
                    objectName = "<color=white><size=40>Read - 'E'</size></color>";

                }

                else
                {
                    enter = false;
                    objectName = "";
                }
                    
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
                objectName = "";
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






                    else if (c.tag == "ReadableNoteEthan1")
                    {
                        Debug.Log("Ethan's First Note Found!");

                        if (note_Ethan1_Played == false)
                        {
                            StartCoroutine(EthanNote1_Function());
                        }
                        else if (note_Ethan1_Played == true)
                        {
                            Debug.Log("Ethan's First Note Already Played!");
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
                    //else if (c.tag == "LawnMower")
                    //{
                    //    if (!oneDay)
                    //    mood.happyMeter += 0; // Mood will come from Grass Cut, not just sitting on the Mower.
                    //    pCamera.Camera8.enabled = true;
                    //    player.transform.position = new Vector3(-100, -100, -100);

                    //    if (pCamera.Camera8.enabled == true)
                    //    {
                    //        Debug.Log("On Mower is true");
                    //        isMowerActive = true;
                    //        sitting = true;
                    //        chara.m_WalkSpeed = 0;
                    //        oneDay = true;
                    //        mowerSource.Play();
                    //    }
                    //}

                    else if (c.tag == "canvasChair")
                    {
                        if (!oneDay)
                            mood.happyMeter += 0; // Mood will come from Grass Cut, not just sitting on the Mower.
                        pCamera.Camera7.enabled = true;
                        //player.transform.position = new Vector3(-100, -100, -100);

                        if (pCamera.Camera7.enabled == true)
                        {
                            Debug.Log("Painting");
                            sitting = true;
                            chara.m_WalkSpeed = 0;
                            oneDay = true;
                            
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
            //else if (pCamera.Camera8.enabled == true)
            //{
            //    // Sound cues go here.
            //    Debug.Log("On Mower is false");
            //    isMowerActive = false;
            //    //player.transform.Translate(1, 1, 1);
            //    player.transform.position = new Vector3(-33, 2, 2);
            //    pCamera.Camera8.enabled = false;
            //    mowerSource.Stop();
            //}
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
            //else if (pCamera.Camera7.enabled == true)
            //{
            //    player.transform.position = new Vector3(-18, -13, 11);
            //    pCamera.Camera7.enabled = false;
            //}
            else
            {
                //objectName = "<color=white><size=40>Stand Up - 'F'</size></color>";

                sitting = false;

                //chara.m_WalkSpeed = 1 + (mood.happyMeter / 100);
            }
        }  
    }
}

