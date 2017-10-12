using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraOrbiter : MonoBehaviour
{

    // CONSTANTS & VARIABLES.
    public GameObject centreOfRoom; // Cube at centre of room, has no Collider or Mesh.
    public float speed; // Orbit Speed.

    public void Start()
    {

    }

    public void Update()
    {
        // Call Orbit Around Function each frame.
        OrbitAround();
    }

    public void OrbitAround()
    {
        // Orbit around the Cube at the room's centre.
        transform.RotateAround(centreOfRoom.transform.position, Vector3.up, speed * Time.deltaTime); // If Lerped or Slerped, change to Time.time;
    }
}
