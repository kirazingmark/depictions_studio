using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class DoorLever : MonoBehaviour 
{
	//==============================================================
	// Transforms
	//==============================================================

	public Transform doorTransform;
	public Collider leverCollider;

	//==============================================================
	// Audio
	//==============================================================

	public AudioSource AudioSource;

	public AudioClip Opening;
	private float OpeningVolume = 1.0f;
	public AudioClip Closing;
	private float ClosingVolume = 1.0f;

	public AudioClip OpeningSlam;
	private float OpeningSlamVolume = 1.0f;
	public AudioClip ClosingSlam;
	private float ClosingSlamVolume = 1.0f;

	public AudioClip Lever;
	private float LeverVolume = 1.0f;

	//==============================================================
	// Particle system
	//==============================================================

	public ParticleSystem Dust;

	//==============================================================
	// Variables DOORS
	//==============================================================

	// Delay
	public float delayBeforeClose = 1.0f;
	public float delayBeforeOpening = 0.3f;

	// Distance the door should move when activated
	public float distanceToMove = 3.0f;

	// The time taken to move from the start to finish positions
	public float timeDuringOpening = 4.0f;
	public float timeDuringClosing = 1.0f;

	// Door closed and opened positions
	private Vector3 closedPosition;
	private Vector3 openedPosition;

	//LERP Variables
	private float timeStartLerp;
	private float timeSinceStarted;
	private float doorOpeningComplete;
	private float doorClosingComplete;

	// Logic
	private bool doorOpening = false;
	private bool doorClosing = false;
	private bool doorOpen = false;

	// Distance to lever
	public float distanceToLever = 3.0f;

	// Door stays open
	public bool stayOpen = false;

	//==============================================================
	// Variables Lever
	//==============================================================

	private Animator leverAnim;

	//==============================================================
	// Init positions
	//==============================================================

	void Start () 
	{
		// Define positions
		closedPosition = doorTransform.transform.position;
		openedPosition = doorTransform.transform.position + doorTransform.TransformDirection (Vector3.up)*distanceToMove;

		// Get Lever Collider for Raycast
		leverCollider = GetComponent<Collider>();

		// Get the Animator component form the parent of this script
		// and set it to the variable animator	
		leverAnim = GetComponent <Animator>();
	}

	void Update ()
	{
		// if left button pressed on mouse...
		//if (Input.GetMouseButtonDown (0)) 
		//{
		//	Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//	RaycastHit hit;
		//	// Raycast on specific lever collider
		//	if (leverCollider.Raycast(ray, out hit, distanceToLever))	
		//	{
		//		if (!doorOpen && !doorOpening) 
		//		{
		//			doorOpening = true;
		//			StartCoroutine ("LeverDown");
		//		}
		//	}
		//}

		// Close the door after x seconds
		if (doorOpen && !doorClosing && !stayOpen) 
		{
			doorClosing = true;
			StartCoroutine("Delay");
		}
	}

	//==============================================================
	// Coroutines (lever and doors)
	//==============================================================

	// Delay and then closing the door
	IEnumerator Delay () 
	{
		// Delay (seconds) before closing the door
		yield return new WaitForSeconds(delayBeforeClose);
		// Close the door
		StartCoroutine ("CloseDoor", closedPosition);
	}

	// Lever pressed. Let's open the door
	IEnumerator LeverDown () 
	{
		// Pull down the lever
		leverAnim.SetTrigger("Trigger");
		// Play OpeningSound
		AudioSource.PlayOneShot (Lever, LeverVolume);
		// Delay x seconds before opening the door
		yield return new WaitForSeconds(delayBeforeOpening);
		// Open the door
		yield return StartCoroutine ("OpenDoor", openedPosition);
	}

	//==============================================================
	// Open the door
	//==============================================================

	IEnumerator OpenDoor (Vector3 openedPosition) 
	{
		// Reset time
		timeStartLerp = Time.time;
		// Reset LERP Complete
		doorOpeningComplete = 0f;
		// Play OpeningSound
		AudioSource.PlayOneShot (Opening, OpeningVolume);
		// Play DustParticles
		Dust.Play ();

		// Open the door
		while (doorOpeningComplete < 1.0f) 
		{
			timeSinceStarted = Time.time - timeStartLerp;
			doorOpeningComplete = timeSinceStarted / timeDuringOpening;

			doorTransform.transform.position = Vector3.Lerp (closedPosition, openedPosition, doorOpeningComplete);

			yield return null;
		}

		doorOpening = false;
		doorOpen = true;

		AudioSource.Stop();
		AudioSource.PlayOneShot (OpeningSlam, OpeningSlamVolume);
	}

	//==============================================================
	// Close the door
	//==============================================================

	IEnumerator CloseDoor (Vector3 closedPosition) 
	{
		// Reset time
		timeStartLerp = Time.time;
		// Reset LERP Complete
		doorClosingComplete = 0f;
		// Play ClosingSound
		AudioSource.PlayOneShot (Closing, ClosingVolume);

		// Close the door
		while (doorClosingComplete < 1.0f) 
		{
			timeSinceStarted = Time.time - timeStartLerp;
			doorClosingComplete = timeSinceStarted / timeDuringClosing;

			doorTransform.transform.position = Vector3.Lerp (openedPosition, closedPosition, doorClosingComplete);

			yield return null;
		}

		Dust.Play();

		doorClosing = false;
		doorOpen = false;

		AudioSource.Stop();
		AudioSource.PlayOneShot (ClosingSlam, ClosingSlamVolume);
	}
}