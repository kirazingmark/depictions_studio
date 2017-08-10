using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour {
    public Material mat;
    public float Fade = 0;
    public float night = 0;
    public float noise = 0;
    [Range(0.0f, 1.0f)]
    public float rate;
    // Use this for initialization
    void Start () {
        rate = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        mat.SetFloat("_Fade", Fade);
        mat.SetFloat("_NightMode", night);
        mat.SetFloat("_Noise", noise);
        mat.SetFloat("_Rate", rate);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
