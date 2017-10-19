using UnityEngine;
using System.Collections;

public class DoorPressure : MonoBehaviour 
{
	//==============================================================
	// Transforms
	//==============================================================

	public Transform doorTransform;
	public Transform padTransform;

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

	public AudioClip Pad;
	private float PadVolume = 1.0f;

	//==============================================================
	// Particle system
	//==============================================================

	public ParticleSystem Dust;

	//==============================================================
	// Variables DOORS
	//==============================================================

	// Delay
	public float delayBeforeClose = 1.0f;
	public float delayBeforeOpening = 1.0f;

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
	private bool doorClosed = true;

	// Door stays open
	public bool stayOpen = false;

	//==============================================================
	// Variables Pressure Pad
	//==============================================================

	// Distance the door should move when activated
	private float distanceToMovePad = 0.1f;

	// The time taken to move from the start to finish positions
	private float timeDuringPressingPad = 0.5f;

	// Door closed and opened positions
	private Vector3 padPressedPosition;
	private Vector3 padUnPressedPosition;

	//LERP Variables
	private float padTimeStartLerp;
	private float padTimeSinceStarted;
	private float padPressedComplete;

	// Logic
	private bool standOnPad = false;

	//==============================================================
	// Init positions
	//==============================================================

	void Start () 
	{
		// Define positions
		closedPosition = doorTransform.transform.position;
		openedPosition = doorTransform.transform.position + doorTransform.TransformDirection (Vector3.up)*distanceToMove;
		padUnPressedPosition = padTransform.transform.position;
		padPressedPosition = padTransform.transform.position + padTransform.TransformDirection (Vector3.down)*distanceToMovePad;
	}

	void Update ()
	{
		// Close the door after x seconds
		if (doorOpen && !doorClosing && !standOnPad && !stayOpen) 
		{
			doorClosing = true;
			StartCoroutine("Delay");
		}
		// Open the door if stand on the pad when the door is closing
		if (doorClosed && !doorOpening && standOnPad) 
		{
			doorOpening = true;
			StartCoroutine ("OpenDoor", openedPosition);
		}
	}

	//==============================================================
	// On Trigger
	//==============================================================

	void OnTriggerEnter (Collider other) 
	{
		standOnPad = true;
		if (!doorOpen && !doorOpening) 
		{
			doorOpening = true;
			StartCoroutine("PressPadDown");
		}
		else
		{
			StartCoroutine ("PadDown", padPressedPosition);
		}
	}

	void OnTriggerExit (Collider other) 
	{
		standOnPad = false;
		StartCoroutine ("PadUp", padUnPressedPosition);
	}

	//==============================================================
	// Coroutines (pad and doors)
	//==============================================================

	// Delay and then closing the door
	IEnumerator Delay () 
	{
		// Delay (seconds) before closing the door
		yield return new WaitForSeconds(delayBeforeClose);
		// Close the door
		StartCoroutine ("CloseDoor", closedPosition);
	}

	// Pad pressed. Let's open the door
	IEnumerator PressPadDown () 
	{
		// Press down the pad
		yield return StartCoroutine ("PadDown", padPressedPosition);
		// Delay 0.5 seconds before opening the door
		yield return new WaitForSeconds(delayBeforeOpening);
		// Open the door
		yield return StartCoroutine ("OpenDoor", openedPosition);
	}

	IEnumerator PadDown (Vector3 padPressedPosition) 
	{
		// Reset time
		padTimeStartLerp = Time.time;
		// Reset LERP Complete
		padPressedComplete = 0f;
		// Play OpeningSound
		AudioSource.PlayOneShot (Pad, PadVolume);


		while (padPressedComplete < 1.0f) 
		{
			padTimeSinceStarted = Time.time - padTimeStartLerp;
			padPressedComplete = padTimeSinceStarted / timeDuringPressingPad;

			padTransform.transform.position = Vector3.Lerp (padUnPressedPosition, padPressedPosition, padPressedComplete);

			yield return null;
		}
	}

	IEnumerator PadUp (Vector3 padUnPressedPosition) 
	{
		// Reset time
		padTimeStartLerp = Time.time;
		// Reset LERP Complete
		padPressedComplete = 0f;
		// Play OpeningSound
		AudioSource.PlayOneShot (Pad, PadVolume);


		while (padPressedComplete < 1.0f) 
		{
			padTimeSinceStarted = Time.time - padTimeStartLerp;
			padPressedComplete = padTimeSinceStarted / timeDuringPressingPad;

			padTransform.transform.position = Vector3.Lerp (padPressedPosition, padUnPressedPosition, padPressedComplete);

			yield return null;
		}
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
		doorClosed = false;

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
		doorClosed = true;

		AudioSource.Stop();
		AudioSource.PlayOneShot (ClosingSlam, ClosingSlamVolume);
	}
}