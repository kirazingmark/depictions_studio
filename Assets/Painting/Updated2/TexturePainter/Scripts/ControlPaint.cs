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
    public GameObject ScreenCentre;
    //public Slider value;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

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
            ScreenCentre.SetActive(false);
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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
            ScreenCentre.SetActive(true);
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
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
