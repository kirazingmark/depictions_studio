using UnityEngine;
using System.Collections;

public class MouseLookOrbit : MonoBehaviour
{
	private Transform MyCam;
	private float xDeg = 0.0f;
	private float yDeg = 2.5f;
	private Quaternion currentRotation;
	private Quaternion desiredRotation;
	private Quaternion startingRotation;
	public int xMinLimit = -80;
	public int xMaxLimit = 80;
	public int yMinLimit = -70;
	public int yMaxLimit = 70;
	public float xSpeed = 200.0f;
	public float ySpeed = 200.0f;
	public float zoomDampening = 10.0f;
	public float resetSpeed = 5.0f;

	private Quaternion rotation;

	private bool freelook = true;
	private bool rotate = false;

	void Start()
	{
		MyCam = transform;
		startingRotation = MyCam.localRotation;
	}

	void LateUpdate ()
	{
		// Right Mouse Button Down
		if (Input.GetMouseButtonDown (1))
		{
			xDeg = 0f;
			yDeg = 2.5f;
			freelook = true;
			rotate = false;
		}
		// Hold Right Mouse Button
		if (Input.GetMouseButton (1))
		{
			//Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
			Cursor.visible = false;	// Hide MouseCursor
			if (freelook && rotate == false)
				FreeLook ();
		}
		// Right Mouse Button Released
		if (Input.GetMouseButtonUp (1))
		{
			//Cursor.lockState = CursorLockMode.None; // Unlock MouseCursor to center
			Cursor.visible = true;	// Show MouseCursor
			freelook = false;
			rotate = true;
		}
		if (rotate)
			ResetRotate ();
	}

	private void ResetRotate ()
	{
		// Center camera
		xDeg = 0.0f;
		yDeg = 0.0f;
		currentRotation = MyCam.localRotation;
		rotation = Quaternion.Lerp(currentRotation, startingRotation, Time.deltaTime * resetSpeed);
		MyCam.localRotation = rotation;
	}

	private void FreeLook ()
	{
		// Orbiting camera
		xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

		//Clamp the vertical axis for the orbit
		xDeg = ClampAngle(xDeg, xMinLimit, xMaxLimit);
		yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

		// set camera rotation
		desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
		currentRotation = MyCam.localRotation;
		rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
		MyCam.localRotation = rotation;
	}

	private static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}
}