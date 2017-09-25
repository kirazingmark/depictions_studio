// Copyright 2017 Integrity Software and Games, LLC.

using UnityEngine;
using System.Collections;

public class SmoothCamFollow : MonoBehaviour 
{
	public Transform target;
	private Transform cam;

	public float posSpeed = 1.0F;
	public float rotSpeed = 1.0F;

	// Use this for initialization
	void Start () 
	{
		cam = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		// position movement
		cam.position = Vector3.Lerp (cam.position, target.position, (posSpeed * Time.deltaTime));

		// rotation movement
		cam.rotation = Quaternion.Lerp (cam.rotation, target.rotation, (rotSpeed * Time.deltaTime));
	}
}
