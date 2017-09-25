using UnityEngine;
using System.Collections;

public class CubeInstantiator : MonoBehaviour 
{
	public static void AddCube (Vector3 loc) 
	{
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.transform.position = loc;
	}
}