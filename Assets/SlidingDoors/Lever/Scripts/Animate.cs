using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Animate : MonoBehaviour 
{
	// Create empty variable type Animator with name animator
	private Animator Lever;

	// Get the Animator component form the parent of this script
	// and set it to the variable animator	
	void Start()
	{
		Lever = GetComponent <Animator>();
	}
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Lever.SetTrigger("Trigger");
		}
	}
}