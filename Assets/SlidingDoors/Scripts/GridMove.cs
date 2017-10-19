using UnityEngine;
using System.Collections;

public class GridMove : MonoBehaviour
{
	//==============================================================
	// Keys to move player
	//==============================================================

	public string forw = "s";
	public string backw = "x";
	public string left = "z";
	public string right = "c";
	public string t_left = "a";
	public string t_right = "d";

	//==============================================================
	// GridMove Variables
	//==============================================================

	//The start and finish positions for the interpolation
	private Vector3 curPos;
	private Vector3 newPos;
	// How far the object should move when key is pressed
	public float distanceToMove = 4.0f;
	// The Time.time value when we started the interpolation
	private float timeStartLerp;
	// The time taken to move from the start to finish positions
	public float timeDuringWalk = 0.4f;
	// Whether we are currently interpolating or not
	private bool walk = false;
	// We will not be able to hit another key during move
	private bool moving = false;

	// ****** Rotate
	private float degree = 0.0f;
	// The time taken to rotate
	public float timeDuringRotate = 0.3f;
	// Whether we are currently interpolating or not
	private bool rotate = false;

	// ****** Bounce
	private Vector3 bouncePos;
	// The time taken to bounce
	public float timeDuringBounce = 0.3f;
	// Whether we are currently interpolating or not
	private bool bounce = false;

	//==============================================================
	// Init positions
	//==============================================================

	void Start ()
	{
		curPos = transform.position;
	}

	//==============================================================
	// Update and move player
	//==============================================================

	void Update ()
	{
		if (Input.GetKey (forw) || Input.GetKey (KeyCode.UpArrow)) 
		{
			playerMoveForward ();
		}
		if (Input.GetKey (backw) || Input.GetKey (KeyCode.DownArrow)) 
		{
			playerMoveBackward ();
		}
		if (Input.GetKey (left) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			playerStrafeLeft ();
		}
		if (Input.GetKey (right) || Input.GetKey (KeyCode.RightArrow)) 
		{
			playerStrafeRight ();
		}
		if (Input.GetKey (t_left))
		{
			playerRotateLeft ();
		}
		if (Input.GetKey (t_right)) 
		{
			playerRotateRight ();
		}

		if (Input.GetKey(KeyCode.Escape))
		{ 
			Application.Quit ();
		}
		MovePlayer ();
		RotatePlayer ();
		BouncePlayer ();
	}

	public void playerMoveForward ()
	{
		if (moving == false && rotate == false)
			MoveForward ();
	}
	public void playerMoveBackward ()
	{
		if (moving == false && rotate == false)	
			MoveBackward ();
	}
	public void playerStrafeLeft ()
	{
		if (moving == false && rotate == false)	
			StrafeLeft ();
	}
	public void playerStrafeRight ()
	{
		if (moving == false && rotate == false)	
			StrafeRight ();
	}
	public void playerRotateLeft ()
	{
		if (moving == false && walk == false) 
			RotateLeft ();
	}
	public void playerRotateRight ()
	{
		if (moving == false && walk == false) 
			RotateRight ();
	}

	//==============================================================
	// Collision with Doors and Walls
	//==============================================================

	void OnCollisionEnter(Collision x)
	{
		if (x.gameObject.name.Contains ("Door"))
		{
			bouncePos = transform.position;
			bounce = true;
		}
		if (x.gameObject.name.Contains ("Wall"))
		{
			bouncePos = transform.position;
			bounce = true;
		}
	}

	//==============================================================
	// Collision with Empty objects and display text
	//==============================================================

	void OnTriggerEnter (Collider x) 
	{
		if (x.gameObject.name.Contains ("TextAuto"))
		{
			PopupText.DisplayText ("Walk to the door");
		}
		if (x.gameObject.name.Contains ("TextKeyB"))
		{
			PopupText.DisplayText ("Walk to the door and press \"E\"");
		}
		if (x.gameObject.name.Contains ("TextButton"))
		{
			PopupText.DisplayText ("Walk to the door and pull the lever");
		}
		if (x.gameObject.name.Contains ("TextPressure"))
		{
			PopupText.DisplayText ("Step on the pressure pad\nto open the door");
		}
	}
	void OnTriggerExit (Collider x) 
	{
		if (x.gameObject.name.Contains ("Text"))
		{
			PopupText.DisplayText ("");
		}
	}

	//==============================================================
	// Move player in all four directions
	//==============================================================

	private void MovePlayer ()
	{
		if (walk)
		{
			moving = true;

			//We want percentage = 0.0 when Time.time = timeStartLerp and percentage = 1.0 when Time.time = timeStartLerp + timeDuringWalk
			//In other words, we want to know what percentage of "timeDuringWalk" the value "Time.time - timeStartLerp" is.
			float timeSinceStarted = Time.time - timeStartLerp;
			float walkComplete = timeSinceStarted / timeDuringWalk;

			//Perform the actual lerping.  Notice that the first two parameters will always be the same throughout a single lerp-processs 
			//(ie. they won't change until we hit a key again to start another lerp)
			transform.position = Vector3.Lerp (curPos, newPos, walkComplete);

			//When we've completed the lerp, we set moving to false
			if (walkComplete >= 1.0f)
			{
				walk = false;
				moving = false;
			}
		}
	}

	//==============================================================
	// Rotate player
	//==============================================================

	private void RotatePlayer ()
	{
		if (rotate)
		{
			moving = true;

			//We want percentage = 0.0 when Time.time = timeStartLerp and percentage = 1.0 when Time.time = timeStartLerp + timeDuringWalk
			//In other words, we want to know what percentage of "timeDuringWalk" the value "Time.time - timeStartLerp" is.
			float timeSinceStarted = Time.time - timeStartLerp;
			float rotateComplete = timeSinceStarted / timeDuringRotate;

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, degree, 0), rotateComplete);

			//When we've completed the lerp, we set moving to false
			if (rotateComplete >= 1.0f)
			{
				rotate = false;
				moving = false;
			}
		}
	}

	//==============================================================
	// Bouncing back when colliding to wall or door
	//==============================================================

	private void BouncePlayer ()
	{
		if (bounce)
		{
			moving = true;

			//We want percentage = 0.0 when Time.time = timeStartLerp and percentage = 1.0 when Time.time = timeStartLerp + timeDuringWalk
			//In other words, we want to know what percentage of "timeDuringWalk" the value "Time.time - timeStartLerp" is.
			float timeSinceStarted = Time.time - timeStartLerp;
			float bounceComplete = timeSinceStarted / timeDuringBounce;

			//Perform the actual lerping.  Notice that the first two parameters will always be the same throughout a single lerp-processs (ie. they won't change until we hit the space-bar again to start another lerp)
			transform.position = Vector3.Lerp (bouncePos, curPos, bounceComplete);

			//When we've completed the lerp, we set moving to false
			if (bounceComplete >= 1.0f)
			{
				bounce = false;
				walk = false;
				moving = false;
			}
		}
	}

	//==============================================================
	// Set values
	//==============================================================

	private void MoveForward ()
	{
		walk = true;
		timeStartLerp = Time.time;
		//We set the start position to the current position, and the finish to 4 spaces in the 'forward' direction
		curPos = transform.position;
		newPos = transform.position + transform.TransformDirection (Vector3.forward)*distanceToMove;
	}
	private void MoveBackward ()
	{
		walk = true;
		timeStartLerp = Time.time;
		//We set the start position to the current position, and the finish to 4 spaces in the 'forward' direction
		curPos = transform.position;
		newPos = transform.position + transform.TransformDirection (Vector3.back)*distanceToMove;
	}
	private void StrafeLeft ()
	{
		walk = true;
		timeStartLerp = Time.time;
		//We set the start position to the current position, and the finish to 4 spaces in the 'forward' direction
		curPos = transform.position;
		newPos = transform.position + transform.TransformDirection (Vector3.left)*distanceToMove;
	}
	private void StrafeRight ()
	{
		walk = true;
		timeStartLerp = Time.time;
		//We set the start position to the current position, and the finish to 4 spaces in the 'forward' direction
		curPos = transform.position;
		newPos = transform.position + transform.TransformDirection (Vector3.right)*distanceToMove;
	}
	private void RotateLeft ()
	{
		rotate = true;
		timeStartLerp = Time.time;
		degree -= 90f;
	}
	private void RotateRight ()
	{
		rotate = true;
		timeStartLerp = Time.time;
		degree += 90f;
	}
}
