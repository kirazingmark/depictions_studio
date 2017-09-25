using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriftRight : MonoBehaviour {

    public float movementSpeed = 0.505f;

    void Update()
    {
        StartCoroutine(VeerRight());
        StartCoroutine(SharpRight());
    }

    IEnumerator VeerRight()
    {
        yield return new WaitForSeconds(27);

        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        yield return null;

    }

    IEnumerator SharpRight()
    {
        yield return new WaitForSeconds(37);

        movementSpeed = 2.0f;

        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        yield return null;

    }
}

