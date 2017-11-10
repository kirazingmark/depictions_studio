using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth_Movement : MonoBehaviour {

    public float _Angle;
    public float _Period;

    private float _Time;

    // Update is called once per frame
    void Update () {
        _Time = _Time + Time.deltaTime;
        float phase = Mathf.Sin(_Time / _Period);
        Debug.Log(phase);

        try {
            transform.localRotation = Quaternion.Euler(new Vector3(phase * _Angle, phase + 0.01f, phase + 0.01f));
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
        
    }
}