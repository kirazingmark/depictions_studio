using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlPaint : MonoBehaviour {

    CameraSwitcher pCamera;
    public TexturePainter tPainter;
    public Button exitButton;
    public Button deleteButoton;
    public Button removeButton;
    public Button stencil1;
    public Button stencil2;
    public Button brush1;
    public Button brush2;
    //public Slider value;

    // Use this for initialization
    void Start () {
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
        
	}

    void Update()
    {
        if (pCamera.Camera7.enabled == true)
        {
            exitButton.gameObject.SetActive(true);
            deleteButoton.gameObject.SetActive(true);
            removeButton.gameObject.SetActive(true);
            brush1.gameObject.SetActive(true);
            brush2.gameObject.SetActive(true);
            //value.gameObject.SetActive(true);
            stencil1.gameObject.SetActive(true);
            stencil2.gameObject.SetActive(true);
        }
        else
        {
            exitButton.gameObject.SetActive(false);
            deleteButoton.gameObject.SetActive(false);
            removeButton.gameObject.SetActive(false);
            brush1.gameObject.SetActive(false);
            brush2.gameObject.SetActive(false);
            //value.gameObject.SetActive(false);
            stencil1.gameObject.SetActive(false);
            stencil2.gameObject.SetActive(false);
        }
    }

    public void exitPainting() //exiting the painting part
    {
        pCamera.Camera7.enabled = false;
    }

    public void delete()
    {
        tPainter.RestartCanvas();
    }

    public void remove()
    {
        tPainter.RemoveStencil();
    }
}
