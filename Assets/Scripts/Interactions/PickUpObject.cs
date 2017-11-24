using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI; // For Notes UI Elements.
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpObject : MonoBehaviour {

    public string playEndingLevel;

    public Camera mainCamera;
    public GameObject carriedObject;
    public bool isCarrying;
    public bool enter;
    public float distance;
    public float smooth;
    public float timer = 5;
    public string sceneName; // Used for restarting the Scene - temporary only.
    public bool reading;
    public float noteMoveSpeed = 100.0f;
    public float noteMoveDuration = 1.00f;
    public bool noteCurrentlyMoving = false;
    public bool noteMaximised = true;

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

    // In-Scene Interactive Notes.
    public GameObject EthanNote1;
    public GameObject EthanNote2;
    public GameObject EthanNote3;
    public GameObject EthanNote4;
    public GameObject EthanNote5;
    public GameObject SophieNote1;
    public GameObject SophieNote2;
    public GameObject SophieNote3;

    // In-Scene Note Illuminators.
    public GameObject Note1Illuminator;
    public GameObject Note2Illuminator;
    public GameObject Note3Illuminator;
    public GameObject Note4Illuminator;
    public GameObject Note5Illuminator;
    public GameObject Note6Illuminator;
    public GameObject Note7Illuminator;
    public GameObject Note8Illuminator;

    // Note Door Lock GameObjects.
    public GameObject Note1_DoorLock; // Bedroom Door.
    public GameObject Note2_3_4_DoorLock1; // Babies Room.
    public GameObject Note5_6_DoorLock1; // Front Door.
    public GameObject Note5_6_DoorLock2; // Side Door Left.

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

    // Note IEnumerator Currently Running.
    public bool Ethan_Note1_CurrentlyRunning;
    public bool Ethan_Note2_CurrentlyRunning;
    public bool Ethan_Note3_CurrentlyRunning;
    public bool Ethan_Note4_CurrentlyRunning;
    public bool Ethan_Note5_CurrentlyRunning;
    public bool Sophie_Note1_CurrentlyRunning;
    public bool Sophie_Note2_CurrentlyRunning;
    public bool Sophie_Note3_CurrentlyRunning;

    // Note Active Flags.
    public bool isEthanNote1Active;
    public bool isEthanNote2Active;
    public bool isEthanNote3Active;
    public bool isEthanNote4Active;
    public bool isEthanNote5Active;
    public bool isSophieNote1Active;
    public bool isSophieNote2Active;
    public bool isSophieNote3Active;

    // Temorary Door GameObjects due to issues with the Detection & Door Scripts.
    public GameObject Door_Bedroom;
    public GameObject Door_BabiesRoom;
    public GameObject Door_FrontDoor;
    public GameObject Door_SideDoorLeft;
    public GameObject Door_SideDoorRight;
    public GameObject Door_Studio;

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

        // Set Temporary Door GameObjects to true by default.
        Door_Bedroom.SetActive(true);
        Door_BabiesRoom.SetActive(true);
        Door_FrontDoor.SetActive(true);
        Door_SideDoorLeft.SetActive(true);
        Door_SideDoorRight.SetActive(true);
        Door_Studio.SetActive(true);

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

        // Note In-Scene Interactive elements being set.
        EthanNote1.SetActive(true);
        EthanNote2.SetActive(true);
        EthanNote3.SetActive(false);
        EthanNote4.SetActive(false);
        EthanNote5.SetActive(false);
        SophieNote1.SetActive(false);
        SophieNote2.SetActive(false);
        SophieNote3.SetActive(false);

        // Note In-Scene Illuminator elements being set.
        Note1Illuminator.SetActive(true);
        Note2Illuminator.SetActive(true);
        Note3Illuminator.SetActive(false);
        Note4Illuminator.SetActive(false);
        Note5Illuminator.SetActive(false);

        // Note Active Flags.
        isEthanNote1Active = true;
        isEthanNote2Active = true;
        isEthanNote3Active = false;
        isEthanNote4Active = false;
        isEthanNote5Active = false;
        isSophieNote1Active = false;
        isSophieNote2Active = false;
        isSophieNote3Active = false;

        // Note IEnumerator Currently Running.
        Ethan_Note1_CurrentlyRunning = false;
        Ethan_Note2_CurrentlyRunning = false;
        Ethan_Note3_CurrentlyRunning = false;
        Ethan_Note4_CurrentlyRunning = false;
        Ethan_Note5_CurrentlyRunning = false;
        Sophie_Note1_CurrentlyRunning = false;
        Sophie_Note2_CurrentlyRunning = false;
        Sophie_Note3_CurrentlyRunning = false;

        // Note Door Lock GameObjects being set to TRUE by default.
        Note1_DoorLock.SetActive(true);
        Note2_3_4_DoorLock1.SetActive(true);
        Note5_6_DoorLock1.SetActive(true);
        Note5_6_DoorLock2.SetActive(true);
    }

    IEnumerator MoveNoteUpwards()
    {
        //yield return new WaitForSeconds(58);
        if (noteMaximised == true && noteCurrentlyMoving == false && reading == true)
        {

            noteCurrentlyMoving = true;

            float elapsedTime = 0f;
            while (elapsedTime < noteMoveDuration)
            {
                if (isEthanNote1Active == true)
                {
                    Ethan_Note1.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Ethan_Note1.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isEthanNote2Active == true)
                {
                    Ethan_Note2.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Ethan_Note2.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isEthanNote3Active == true)
                {
                    Ethan_Note3.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Ethan_Note3.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if(isEthanNote4Active == true)
                {
                    Ethan_Note4.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Ethan_Note4.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isEthanNote5Active == true)
                {
                    Ethan_Note5.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Ethan_Note5.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isSophieNote1Active == true)
                {
                    Sophie_Note1.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Sophie_Note1.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isSophieNote2Active == true)
                {
                    Sophie_Note2.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Sophie_Note2.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isSophieNote3Active == true)
                {
                    Sophie_Note3.transform.Translate(Vector3.down * Time.deltaTime * 650);
                    //Sophie_Note3.transform.Translate(Vector3.left * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            noteCurrentlyMoving = false;
            noteMaximised = false;
        }
        else if (noteMaximised == false && noteCurrentlyMoving == false && reading == true)
        {
            noteCurrentlyMoving = true;

            float elapsedTime = 0f;
            while (elapsedTime < noteMoveDuration)
            {
                if (isEthanNote1Active == true)
                {
                    Ethan_Note1.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Ethan_Note1.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isEthanNote2Active == true)
                {
                    Ethan_Note2.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Ethan_Note2.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isEthanNote3Active == true)
                {
                    Ethan_Note3.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Ethan_Note3.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isEthanNote4Active == true)
                {
                    Ethan_Note4.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Ethan_Note4.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isEthanNote5Active == true)
                {
                    Ethan_Note5.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Ethan_Note5.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isSophieNote1Active == true)
                {
                    Sophie_Note1.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Sophie_Note1.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isSophieNote2Active == true)
                {
                    Sophie_Note2.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Sophie_Note2.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }
                else if (isSophieNote3Active == true)
                {
                    Sophie_Note3.transform.Translate(Vector3.up * Time.deltaTime * 650);
                    //Sophie_Note3.transform.Translate(Vector3.right * Time.deltaTime * 700);
                    elapsedTime += Time.deltaTime;
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            noteCurrentlyMoving = false;
            noteMaximised = true;
        }
    }

    // ETHAN BEDROOM NOTE FUNCTION.
    public IEnumerator EthanNote1_Function()
    {
        noteMaximised = true;
        isEthanNote1Active = true;
        Debug.Log("Ethan 1 Function Called!");

        Ethan_Note1_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Ethan_Note1.SetActive(true);

        audioPlayBack.clip = note_Ethan1;
        audioPlayBack.PlayOneShot(note_Ethan1, 1.0f);

        reading = true;
        
        //Time.timeScale = 0.0f;

        yield return new WaitForSeconds(audioPlayBack.clip.length);
        //Time.timeScale = 1.0f;
        audioPlayBack.clip = doorUnlock;
        audioPlayBack.Play();

        // Disable Note 1 Door Lock GameObject.
        Note1_DoorLock.SetActive(false);
        //Door_Bedroom.SetActive(false);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Ethan_Note1.SetActive(false);
        Note1Illuminator.SetActive(false);

        reading = false;

        //Time.timeScale = 1.0f;

        // Change Ethan Note 1 Flag.
        note_Ethan1_Played = true;
        EthanNote2.SetActive(true);

        Ethan_Note1_CurrentlyRunning = false;
        isEthanNote1Active = false;
    }

    // ETHAN KITCHEN NOTE FUNCTION.
    public IEnumerator EthanNote2_Function()
    {
        noteMaximised = true;
        isEthanNote2Active = true;
        Debug.Log("Ethan 2 Function Called!");

        Ethan_Note2_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Ethan_Note2.SetActive(true);

        reading = true;

        audioPlayBack.clip = note_Ethan2;
        audioPlayBack.PlayOneShot(note_Ethan2, 1.0f);

        yield return new WaitForSeconds(audioPlayBack.clip.length);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Ethan_Note2.SetActive(false);

        reading = false;

        // Change Ethan Note 2 Flag.
        note_Ethan2_Played = true;

        // Change Next Note Visibility Flag.
        isEthanNote3Active = true;
        EthanNote3.SetActive(true);
        Note2Illuminator.SetActive(false);
        Note3Illuminator.SetActive(true);

        // Check to see if conditions to unlock Babies Room have been met.
        if (note_Ethan2_Played == true && note_Ethan3_Played == true && note_Ethan4_Played == true)
        {
            Note2_3_4_DoorLock1.SetActive(false);
            audioPlayBack.clip = doorUnlock;
            audioPlayBack.Play();
        }
        else
        {
        }

        Ethan_Note2_CurrentlyRunning = false;
        isEthanNote2Active = false;
    }

    // ETHAN DINNING ROOM NOTE FUNCTION.
    public IEnumerator EthanNote3_Function()
    {
        noteMaximised = true;
        isEthanNote3Active = true;
        Debug.Log("Ethan 3 Function Called!");

        Ethan_Note3_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Ethan_Note3.SetActive(true);

        reading = true;

        audioPlayBack.clip = note_Ethan3;
        audioPlayBack.PlayOneShot(note_Ethan3, 1.0f);

        

        yield return new WaitForSeconds(audioPlayBack.clip.length);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Ethan_Note3.SetActive(false);

        reading = false;

        // Change Ethan Note 3 Flag.
        note_Ethan3_Played = true;

        // Change Next Note Visibility Flag.
        isEthanNote4Active = true;
        EthanNote4.SetActive(true);
        Note3Illuminator.SetActive(false);
        Note4Illuminator.SetActive(true);

        // Check to see if conditions to unlock Babies Room have been met.
        if (note_Ethan2_Played == true && note_Ethan3_Played == true && note_Ethan4_Played == true)
        {
            Note2_3_4_DoorLock1.SetActive(false);
            audioPlayBack.clip = doorUnlock;
            audioPlayBack.Play();
        }
        else
        {
        }

        Ethan_Note3_CurrentlyRunning = false;
        isEthanNote3Active = false;
    }

    // ETHAN LOUNGE NOTE FUNCTION.
    public IEnumerator EthanNote4_Function()
    {
        noteMaximised = true;
        isEthanNote4Active = true;
        Debug.Log("Ethan 4 Function Called!");

        Ethan_Note4_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Ethan_Note4.SetActive(true);

        reading = true;

        audioPlayBack.clip = note_Ethan4;
        audioPlayBack.PlayOneShot(note_Ethan4, 1.0f);

        

        yield return new WaitForSeconds(audioPlayBack.clip.length);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Ethan_Note4.SetActive(false);

        // Change Ethan Note 4 Flag.
        note_Ethan4_Played = true;

        // Change Next Note Visibility Flag.
        isEthanNote5Active = true;
        EthanNote5.SetActive(true);
        Note4Illuminator.SetActive(false);
        Note5Illuminator.SetActive(true);

        reading = false;

        // Check to see if conditions to unlock Babies Room have been met.
        if (note_Ethan2_Played == true && note_Ethan3_Played == true && note_Ethan4_Played == true)
        {
            Note2_3_4_DoorLock1.SetActive(false);
            //Door_BabiesRoom.SetActive(false);
            audioPlayBack.clip = doorUnlock;
            audioPlayBack.Play();
        }
        else
        {
        }

        Ethan_Note4_CurrentlyRunning = false;
        isEthanNote4Active = false;
    }

    // ETHAN BABIES ROOM FUNCTION 1.
    public IEnumerator EthanNote5_Function()
    {
        noteMaximised = true;
        isEthanNote5Active = true;
        Debug.Log("Ethan 5 Function Called!");

        Ethan_Note5_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Ethan_Note5.SetActive(true);

        reading = true;

        audioPlayBack.clip = note_Ethan5;
        audioPlayBack.PlayOneShot(note_Ethan5, 1.0f);

        

        yield return new WaitForSeconds(audioPlayBack.clip.length);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Ethan_Note5.SetActive(false);

        // Change Ethan Note 5 Flag.
        note_Ethan5_Played = true;

        // Change Next Note Visibility Flag.
        Note5Illuminator.SetActive(false);
        Note6Illuminator.SetActive(true);
        isSophieNote1Active = true;
        SophieNote1.SetActive(true);

        reading = false;

        // Check to see if conditions to unlock Exterior Doors have been met.
        if (note_Ethan5_Played == true)
        {
            Note5_6_DoorLock1.SetActive(false);
            Note5_6_DoorLock2.SetActive(false);
            audioPlayBack.clip = doorUnlock;
            audioPlayBack.Play();

            Note5_6_DoorLock1.SetActive(false);
            Note5_6_DoorLock2.SetActive(false);
            //Door_FrontDoor.SetActive(false);
            //Door_SideDoorLeft.SetActive(false);
            //Door_SideDoorRight.SetActive(false);
            //Door_Studio.SetActive(false);
            audioPlayBack.clip = doorUnlock;
            audioPlayBack.Play();
        }
        else
        {
        }

        Ethan_Note5_CurrentlyRunning = false;
        isEthanNote5Active = false;
    }

    // SOPHIE STUDIO NOTE FUNCTION.
    public IEnumerator SophieNote1_Function()
    {
        noteMaximised = true;
        isSophieNote1Active = true;
        Debug.Log("Sophie 1 Function Called!");

        Sophie_Note1_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Sophie_Note1.SetActive(true);

        reading = true;

        audioPlayBack.clip = note_Ethan1;
        audioPlayBack.PlayOneShot(note_Sophie1, 1.0f);

        

        yield return new WaitForSeconds(audioPlayBack.clip.length);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Sophie_Note1.SetActive(false);

        reading = false;

        // Change Sophie Note 1 Flag.
        note_Sophie1_Played = true;

        // Change Next Note Visibility Flag.
        isSophieNote2Active = true;
        SophieNote2.SetActive(true);
        Note6Illuminator.SetActive(false);
        Note7Illuminator.SetActive(true);

        Sophie_Note1_CurrentlyRunning = false;
        isSophieNote1Active = false;
    }

    // SOPHIE POND NOTE FUNCTION.
    public IEnumerator SophieNote2_Function()
    {
        noteMaximised = true;
        isSophieNote2Active = true;
        Debug.Log("Sophie 2 Function Called!");

        Sophie_Note2_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Sophie_Note2.SetActive(true);

        reading = true;

        audioPlayBack.clip = note_Sophie2;
        audioPlayBack.PlayOneShot(note_Sophie2, 1.0f);

        

        yield return new WaitForSeconds(audioPlayBack.clip.length);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Sophie_Note2.SetActive(false);

        reading = false;

        // Change Sophie Note 2 Flag.
        note_Sophie2_Played = true;
        Note7Illuminator.SetActive(false);
        Note8Illuminator.SetActive(true);

        // Change Next Note Visibility Flag.
        isSophieNote3Active = true;
        SophieNote3.SetActive(true);

        Sophie_Note2_CurrentlyRunning = false;
        isSophieNote2Active = false;
    }

    // SOPHIE TREE NOTE FUNCTION.
    public IEnumerator SophieNote3_Function()
    {
        noteMaximised = true;
        isSophieNote3Active = true;
        Debug.Log("Sophie 3 Function Called!");

        Sophie_Note3_CurrentlyRunning = true;

        // Enable UI Elements here.
        ReadingPanel.SetActive(true);
        Sophie_Note3.SetActive(true);

        reading = true;

        audioPlayBack.clip = note_Ethan1;
        audioPlayBack.PlayOneShot(note_Sophie3, 1.0f);

        

        yield return new WaitForSeconds(audioPlayBack.clip.length);

        // Disable UI Elements here.
        ReadingPanel.SetActive(false);
        Sophie_Note3.SetActive(false);

        reading = false;

        // Change Sophie Note 3 Flag.
        note_Sophie3_Played = true;

        Sophie_Note3_CurrentlyRunning = false;
        isSophieNote3Active = false;
    }

    // Update is called once per frame
    void Update () {

        if (note_Sophie1_Played == true && note_Sophie2_Played == true && note_Sophie3_Played == true)
        {

            EndingCutscene();
        }

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

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene(sceneName);
        //}

        if (Input.GetKeyDown("q"))
        {
            StartCoroutine(MoveNoteUpwards());
        }

        //if (Ethan_Note1_CurrentlyRunning == true)
        //{
        //    Time.timeScale = 0.0f;
        //}
        //else
        //{
        //    Time.timeScale = 1.0f;
        //}
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

    public void EndingCutscene()
    {

        SceneManager.LoadScene(playEndingLevel);
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

                        if (note_Ethan1_Played == false && Ethan_Note1_CurrentlyRunning == false)
                        {
                            StartCoroutine(EthanNote1_Function());
                        }
                        else if (note_Ethan1_Played == true)
                        {
                            Debug.Log("Ethan's First Note Already Played!");
                        }
                    }
                    else if (c.tag == "ReadableNoteEthan2")
                    {
                        Debug.Log("Ethan's Second Note Found!");

                        if (note_Ethan2_Played == false && Ethan_Note2_CurrentlyRunning == false)
                        {
                            StartCoroutine(EthanNote2_Function());
                        }
                        else if (note_Ethan2_Played == true)
                        {
                            Debug.Log("Ethan's Second Note Already Played!");
                        }
                    }
                    else if (c.tag == "ReadableNoteEthan3")
                    {
                        Debug.Log("Ethan's Third Note Found!");

                        if (note_Ethan3_Played == false && Ethan_Note3_CurrentlyRunning == false)
                        {
                            StartCoroutine(EthanNote3_Function());
                        }
                        else if (note_Ethan3_Played == true)
                        {
                            Debug.Log("Ethan's Third Note Already Played!");
                        }
                    }
                    else if (c.tag == "ReadableNoteEthan4")
                    {
                        Debug.Log("Ethan's Forth Note Found!");

                        if (note_Ethan4_Played == false && Ethan_Note4_CurrentlyRunning == false)
                        {
                            StartCoroutine(EthanNote4_Function());
                        }
                        else if (note_Ethan4_Played == true)
                        {
                            Debug.Log("Ethan's Forth Note Already Played!");
                        }
                    }
                    else if (c.tag == "ReadableNoteEthan5")
                    {
                        Debug.Log("Ethan's Fifth Note Found!");

                        if (note_Ethan5_Played == false && Ethan_Note5_CurrentlyRunning == false)
                        {
                            StartCoroutine(EthanNote5_Function());
                        }
                        else if (note_Ethan5_Played == true)
                        {
                            Debug.Log("Ethan's Fifth Note Already Played!");
                        }
                    }
                    else if (c.tag == "ReadableNoteSophie1")
                    {
                        Debug.Log("Sophie's First Note Found!");

                        if (note_Sophie1_Played == false && Sophie_Note1_CurrentlyRunning == false)
                        {
                            StartCoroutine(SophieNote1_Function());
                        }
                        else if (note_Sophie1_Played == true)
                        {
                            Debug.Log("Sophie's First Note Already Played!");
                        }
                    }
                    else if (c.tag == "ReadableNoteSophie2")
                    {
                        Debug.Log("Sophie's Second Note Found!");

                        if (note_Sophie2_Played == false && Sophie_Note2_CurrentlyRunning == false)
                        {
                            StartCoroutine(SophieNote2_Function());
                        }
                        else if (note_Sophie2_Played == true)
                        {
                            Debug.Log("Sophie's Second Note Already Played!");
                        }
                    }
                    else if (c.tag == "ReadableNoteSophie3")
                    {
                        Debug.Log("Sophie's Third Note Found!");

                        if (note_Sophie3_Played == false && Sophie_Note3_CurrentlyRunning == false)
                        {
                            StartCoroutine(SophieNote3_Function());
                        }
                        else if (note_Sophie3_Played == true)
                        {
                            Debug.Log("Sophie's Third Note Already Played!");
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

