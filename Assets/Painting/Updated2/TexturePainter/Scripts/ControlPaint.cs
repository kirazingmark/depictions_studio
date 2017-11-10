using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlPaint : MonoBehaviour {

    CameraSwitcher pCamera;
    public TexturePainter tPainter;
    public List<Button> paintingButton = new List<Button>();
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
           
            foreach(Button but in paintingButton)
            {
                but.gameObject.SetActive(true);
            }
            
            ScreenCentre.SetActive(false);
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
           
            foreach (Button but in paintingButton)
            {
                but.gameObject.SetActive(false);
            }
            ScreenCentre.SetActive(true);
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    public void exitPainting() //exiting the painting part
    {
        pCamera.Camera7.enabled = false;
    }

}
