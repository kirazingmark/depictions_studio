using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    GameObject mainCamera;
	GameObject carriedObject;
    GameObject FirstPersonController;
    bool carrying;
    CameraSwitcher pCamera;
    public float distance;
	public float smooth;
    public float rayDistance = 3.0f; // Set in the Inspector as well.
    public float startYRotation;
    public float deltaRotation;
    public float yRotation;
    public float previousUp;
    public Quaternion offset;

    // Use this for initialization
    void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
        FirstPersonController = GameObject.FindWithTag("Player");
        pCamera = GameObject.FindWithTag("Player").GetComponent<CameraSwitcher>();
    }
	
	// Update is called once per frame
	void Update () {
		if(carrying) {
			carry(carriedObject);
			checkDrop();
            //rotateObject();
		} else {
			pickup();
		}
	}

	void rotateObject() {
		carriedObject.transform.Rotate(5,10,15);
	}

    void carry(GameObject o) {
		o.transform.position = Vector3.Slerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth); // Changed from Lerp.

        deltaRotation = previousUp - mainCamera.transform.eulerAngles.y;
        yRotation = startYRotation - deltaRotation;
        Quaternion target = Quaternion.Euler(0, yRotation, 0);
        o.transform.rotation = Quaternion.Slerp(o.transform.rotation, target, Time.deltaTime * 3);
        carriedObject.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        if(carrying == true && Vector3.Distance(o.gameObject.transform.position, mainCamera.transform.position) < 2) {

            Debug.Log("Carrying = " + carrying);
            FirstPersonController.transform.position += -transform.forward * Time.deltaTime * 5; // Used to keep Player from merging into the carried object, so if they come within 2 units they are pushed backwards.
        }

        o.transform.rotation = Quaternion.identity;
    }

    void pickup() {
		if(Input.GetKeyDown (KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, rayDistance)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
                Chair c = hit.collider.GetComponent<Chair>();
                if (p != null && Vector3.Distance(p.gameObject.transform.position, mainCamera.transform.position) < 3) {
					carrying = true;
					carriedObject = p.gameObject;
					p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    previousUp = mainCamera.transform.eulerAngles.y;
                    startYRotation = carriedObject.transform.eulerAngles.y;
                }
                if(c!= null && Vector3.Distance(c.gameObject.transform.position, mainCamera.transform.position) < 3)
                {
                    Camera.main.enabled = false;
                    pCamera.Camera7.enabled = true;
                }

			}
		}
	}

	void checkDrop() {
		if(Input.GetKeyDown (KeyCode.E)) {
			dropObject();
		}
	}

	void dropObject() {
		carrying = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0); // If Physics issue can be resolved, this can be removed.
        carriedObject = null;
    }
}
