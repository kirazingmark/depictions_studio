using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_2_AlternateMovement : MonoBehaviour {

    public float smooth = 1f;
    public float speedAdjuster = 0.1f; // Was 0.25f.
    public bool turnRight1Flag = false;
    public bool turnLeft1Flag = false;
    public bool turnRight2Flag = false;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
        StartCoroutine(changeTurnRight1Flag());
        StartCoroutine(changeTurnLeft1Flag());
        StartCoroutine(changeTurnRight2Flag());
    }

    void Update()
    {
        if (turnRight1Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(5, Vector3.left);
            turnRight1Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (turnLeft1Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(90, Vector3.down);
            turnLeft1Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 6));

        if (turnRight2Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(65, Vector3.up);
            turnRight2Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 6));
    }

    IEnumerator changeTurnRight1Flag()
    {
        yield return new WaitForSeconds(16);

        turnRight1Flag = true;

        yield return null;
    }

    IEnumerator changeTurnLeft1Flag()
    {
        yield return new WaitForSeconds(21);

        turnLeft1Flag = true;

        yield return null;
    }

    IEnumerator changeTurnRight2Flag()
    {
        yield return new WaitForSeconds(33);

        turnRight2Flag = true;

        yield return null;
    }
}
