using UnityEngine;
using System.Collections;

public class DoorHotkey : MonoBehaviour 
{
	//==============================================================
	// Transforms
	//==============================================================

	public Transform doorTransform;

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

	//==============================================================
	// Particle system
	//==============================================================

	public ParticleSystem Dust;

	//==============================================================
	// Variables DOORS
	//==============================================================

	public string key = "e";

	// Delay
	public float delayBeforeClose = 1.0f;

	// Distance the door should move when activated
	public float distanceToMove = 3.0f;

	// The time taken to move from the start to finish positions
	public float timeDuringOpening = 4.0f;
	public float timeDuringClosing = 1.0f;

	// Door closed position
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

	// Door stays open
	public bool stayOpen = false;

	//==============================================================
	// Init positions
	//==============================================================

	void Start () 
	{
		// Define positions
		closedPosition = doorTransform.transform.position;
		openedPosition = doorTransform.transform.position + doorTransform.TransformDirection (Vector3.up)*distanceToMove;
	}

	void Update ()
	{
		// Close the door after x seconds
		if (doorOpen && !doorClosing && !stayOpen) 
		{
			doorClosing = true;
			StartCoroutine("Delay");
		}
	}

	//==============================================================
	// On Trigger
	//==============================================================

	void OnTriggerStay (Collider other) 
	{
		if (Input.GetKey (key))
		{
			if (!doorOpen && !doorOpening)
			{
				doorOpening = true;
				StartCoroutine ("OpenDoor", openedPosition);
			}
			if (doorOpen && !doorClosing && !stayOpen) 
			{
				doorClosing = true;
				StartCoroutine ("CloseDoor", closedPosition);
			}
		}
	}

	//==============================================================
	// Coroutines
	//==============================================================

	// Delay before closing the door
	IEnumerator Delay () 
	{
		// Delay (seconds)
		yield return new WaitForSeconds(delayBeforeClose);
		// Close the door
		yield return StartCoroutine ("CloseDoor", closedPosition);
	}

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