using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarForwardMovement : MonoBehaviour {

    public float movementSpeed = 10;

    void Start()
    {
        StartCoroutine(SlowDown());
    }

    void Update()
    {

        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);

    }

    IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(36);

        movementSpeed = 2;

        yield return null;
    }
}
