using UnityEngine;
using System.Collections;

public class CreatePlaneMesh : MonoBehaviour 
{
	public float width = 50f;
	public float height = 50f;

	void Start ()
	{
		// setup
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;

		// create vertices
		Vector3[] vertices = {
			new Vector3 (0, 0, 0),
			new Vector3 (width, 0, 0),
			new Vector3 (0, height, 0),
			new Vector3 (width, height, 0)
		};

		// create triangles
		int[] triangles = {0, 2, 1, 1, 2, 3};

		// recalculate normals
		Vector3[] normals = {-Vector3.forward, -Vector3.forward, -Vector3.forward, -Vector3.forward};

		// UVs for texturing
		Vector2[] uv = {new Vector2 (0, 0),	new Vector2 (1, 0),	new Vector2 (0, 1),	new Vector2 (1, 1)};

		// assign arrays
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
	}
}
