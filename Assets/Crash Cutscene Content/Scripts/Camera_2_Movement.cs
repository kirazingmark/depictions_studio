using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_2_Movement : MonoBehaviour {

    // CONSTANTS AND VARIABLES.
    public float smooth = 10f;
    public float speedAdjuster = 0.1f; // Was 0.25f.
    public bool turnRight1Flag = false;
    public bool turnLeft1Flag = false;
    public bool turnRight2Flag = false;
    public bool lookDownFlag = false;
    public bool lookUpFlag = false;

    public bool turnCentre1 = false;
    public bool turnCentre2 = false;
    public bool turnCentre3 = false;
    public bool turnBackLeft = false;
    public bool turnBackRight = false;
    public bool turnRight3Flag = false;
    public bool turnLeft2Flag = false;
    public bool turnRight4Flag = false;
    public bool cameraCrashFlag = false;

    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
        StartCoroutine(changeTurnRight1Flag());
        StartCoroutine(changeTurnCentre1Flag());
        StartCoroutine(changeTurnLeft1Flag());
        StartCoroutine(changeLookDownFlag());
        StartCoroutine(changeLookUpFlag());
        StartCoroutine(changeTurnRight2Flag());
        StartCoroutine(changeTurnRight3Flag());
        StartCoroutine(changeTurnLeft2Flag());
        StartCoroutine(changeTurnRight4Flag());
        StartCoroutine(changeCameraCrashSpinFlag());
    }

    void Update()
    {
        if (turnRight1Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(25, Vector3.up);
            turnRight1Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (turnCentre1 == true)
        {
            targetRotation *= Quaternion.AngleAxis(25, Vector3.down);
            turnCentre1 = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (turnLeft1Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(65, Vector3.down);
            turnLeft1Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (lookDownFlag == true)
        {
            targetRotation *= Quaternion.AngleAxis(20, Vector3.right);
            lookDownFlag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (lookUpFlag == true)
        {
            targetRotation *= Quaternion.AngleAxis(20, Vector3.left);
            lookUpFlag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (turnRight2Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(65, Vector3.up);
            turnRight2Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (turnRight3Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(65, Vector3.up);
            turnRight3Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (turnLeft2Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(130, Vector3.down);
            turnLeft2Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (turnRight4Flag == true)
        {
            targetRotation *= Quaternion.AngleAxis(65, Vector3.up);
            turnRight4Flag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));

        if (cameraCrashFlag == true)
        {
            targetRotation *= Quaternion.AngleAxis(150, Vector3.left); // Was up.
            cameraCrashFlag = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedAdjuster * smooth * (Time.deltaTime * 2));
    }

    // Turn to Right.
    IEnumerator changeTurnRight1Flag()
    {
        yield return new WaitForSeconds(11); // Was 16.

        turnRight1Flag = true;

        yield return null;
    }

    IEnumerator changeTurnCentre1Flag()
    {
        yield return new WaitForSeconds(13);

        turnCentre1 = true;

        yield return null;
    }

    IEnumerator changeTurnLeft1Flag()
    {
        yield return new WaitForSeconds(15);

        turnLeft1Flag = true;

        yield return null;
    }

    IEnumerator changeLookDownFlag()
    {
        yield return new WaitForSeconds(15);

       lookDownFlag = true;

        yield return null;
    }

    IEnumerator changeLookUpFlag()
    {
        yield return new WaitForSeconds(20);

        lookUpFlag = true;

        yield return null;
    }

    IEnumerator changeTurnRight2Flag()
    {
        yield return new WaitForSeconds(22);

        turnRight2Flag = true;

        yield return null;
    }

    IEnumerator changeTurnRight3Flag()
    {
        yield return new WaitForSeconds(26);

        turnRight3Flag = true;

        yield return null;
    }

    IEnumerator changeTurnLeft2Flag()
    {
        yield return new WaitForSeconds(29);

        turnLeft2Flag = true;

        yield return null;
    }

    IEnumerator changeTurnRight4Flag()
    {
        yield return new WaitForSeconds(33);

        turnRight4Flag = true;

        yield return null;
    }

    IEnumerator changeCameraCrashSpinFlag()
    {
        yield return new WaitForSeconds(36); // Was 36.

        cameraCrashFlag = true;

        yield return null;
    }
}
