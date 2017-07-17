using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PARTICLESYSTEM ON-OFF CONTROLLER.
    // PLACE THIS SCRIPT ON THE PARTICLESYSTEM IN THE SCENE THAT WILL TOGGLE ON & OFF.

public class TestScript9 : MonoBehaviour
{

    // CONSTANTS & VARIABLES.

    public ParticleSystem vent;

    // Use this for initialization
    void Start()
    {

        vent = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        // Thrust button pressed, start particle system
        if (Input.GetKeyDown("u"))
        {
            PlayThrusters();
        }
        // Thrust button released, deactivate particle system
        if (Input.GetKeyDown("j"))
        {
            StopThrusters();
        }
    }

    void PlayThrusters()
    {
        vent.Play();
        vent.enableEmission = true;
    }
    void StopThrusters()
    {
        vent.enableEmission = false;
        vent.Stop();
    }
}

