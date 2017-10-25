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
    public Slider value;

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
            value.gameObject.SetActive(true);
        }
        else
        {
            exitButton.gameObject.SetActive(false);
            deleteButoton.gameObject.SetActive(false);
            removeButton.gameObject.SetActive(false);
            value.gameObject.SetActive(false);
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
