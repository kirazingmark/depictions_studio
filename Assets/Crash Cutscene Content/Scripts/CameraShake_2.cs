using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake_2 : MonoBehaviour {

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }

        if ((shakeDuration < 6) && (shakeDuration > 5))
        {
            shakeAmount = 0.0015f;
        }

        if ((shakeDuration < 5) && (shakeDuration > 4))
        {
            shakeAmount = 0.0035f;
        }

        if ((shakeDuration < 4) && (shakeDuration > 3))
        {
            shakeAmount = 0.005f;
        }
    }
}