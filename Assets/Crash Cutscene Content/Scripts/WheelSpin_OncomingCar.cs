using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin_OncomingCar : MonoBehaviour {

    public float speed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.left, speed * Time.deltaTime);
    }
}
