using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_2_Movement : MonoBehaviour {

    public float smooth = 1f;
    public float speedAdjuster = 0.1f; // Was 0.25f.
    public bool turnRight1Flag = false;
    public bool turnLeft1Flag = false;
    public bool turnRight2Flag = false;
    public bool lookDownFlag = false;
    public bool lookUpFlag = false;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
        StartCoroutine(changeTurnRight1Flag());
        StartCoroutine(changeTurnLeft1Flag());
        StartCoroutine(changeTurnRight2Flag());
        StartCoroutine(changeLookDownFlag());
        StartCoroutine(changeLookUpFlag());
    }

    void Update()
    {
        if (turnRight1Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(25, Vector3.up);
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

        if (lookDownFlag == true)
        {
            targetRotation *= Quaternion.AngleAxis(20, Vector3.right);
            lookDownFlag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 6));

        if (lookUpFlag == true)
        {
            targetRotation *= Quaternion.AngleAxis(20, Vector3.left);
            lookUpFlag = false;
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
        yield return new WaitForSeconds(20);

        turnLeft1Flag = true;

        yield return null;
    }

    IEnumerator changeTurnRight2Flag()
    {
        yield return new WaitForSeconds(33);

        turnRight2Flag = true;

        yield return null;
    }

    IEnumerator changeLookDownFlag()
    {
        yield return new WaitForSeconds(22);

       lookDownFlag = true;

        yield return null;
    }

    IEnumerator changeLookUpFlag()
    {
        yield return new WaitForSeconds(26);

        lookUpFlag = true;

        yield return null;
    }
}
