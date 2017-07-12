using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpObject : MonoBehaviour {

    public Camera mainCamera;
    public GameObject carriedObject;
    public bool isCarrying;
    public bool isOpening;
    public bool isTurn;
	public bool haveKey;
    public bool enter;
    public float distance;
    public float smooth;
    public float timer = 5;

    public AudioClip pickup;
    public AudioClip drop;
    public AudioClip teleportFire;
    public AudioClip teleportFireBack;
    AudioSource audioPlayBack;

    public FirstPersonController fps;

    // Use this for initialization
    void Start () {

        audioPlayBack = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void carry(GameObject objects)
    {
        objects.transform.position = Vector3.Lerp(objects.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
    }

    void PickUp()
    {
		if(Input.GetButtonDown("PickUp"))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit,10))
            {
                PickUpable p = hit.collider.GetComponent<PickUpable>();
                if(p != null)
                {
                    audioPlayBack.PlayOneShot(pickup, 1.0F);
                    isCarrying = true;
                    carriedObject = p.gameObject;
                    p.rb.isKinematic = true;
                }
            }
        }
    }

    void checkDrop()
    {
		if(Input.GetButtonDown("PickUp"))
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



}
