using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class paintGM : MonoBehaviour {

    public Transform baseDot;
    public KeyCode mouseLeft;
    public static string toolType;
    [SerializeField] public static Color currentColor;
    public static float currentScale = 0.01f;

    public Texture2D text2d;
    public Texture3D text3d;

    public GameObject texture1;

    Vector2 lastMousePos;

    bool startPaint;

    public FirstPersonController chara;
    public bool onSomething;
    public CameraSwitcher pCamera;
    public MouseLook mLook;
    bool oneDay;
    public PickUpObject po;

    Mood mood;

    public GameObject whiteUselessDot;

    // Use this for initialization
    void Start() {

        currentColor = Color.black;
        lastMousePos = new Vector2(-1f, -1f);
        text2d = new Texture2D(1024, 512);
        text3d = new Texture3D(1024, 512, 1, TextureFormat.RGB24, false);
        texture1.GetComponent<Renderer>().materials[0].SetTexture("_MainTex", text2d);
        for (int y = 0; y < 512; y++)
            for (int x = 0; x < 1024; x++)
                text2d.SetPixel(x, y, Color.white);
        text2d.Apply();
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
        oneDay = true;
        po = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpObject>();
        mood = GameObject.FindGameObjectWithTag("Player").GetComponent<Mood>();
    }

    // Update is called once per frame
    void Update() {

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //deleting the whole scene
        if (Input.GetKey(KeyCode.Q))
        {
            for (int y = 0; y < 512; y++)
                for (int x = 0; x < 1024; x++)
                    text2d.SetPixel(x, y, Color.white);
            text2d.Apply();
        }

        // moving the canvas
        if (Input.GetKeyDown(KeyCode.Escape) && mood.painting == false)
        {
            pCamera.Camera7.enabled = false;
            oneDay = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mood.painting == true)
        {
            pCamera.Camera7.enabled = false;
            oneDay = false;
            mood.happyMeter += 5.0f;
        }

        //drawing part

        //if (Input.GetKey(KeyCode.F))
        //{
        //    Camera.main.transform.position = new Vector3(0, 0, 363);
        //    startPaint = true;
        //    CameraSwitcher pCamera;

        //}
        //else if (Input.GetKey(KeyCode.Space))
        //{
        //    startPaint = false;
        //    Camera.main.transform.position = chara.transform.position;
        //}
        //if (startPaint)
        if (pCamera.Camera7.enabled == true)
        {
            chara.m_WalkSpeed = 0;
            mood.painting = true;
            if (oneDay)
            {
                whiteUselessDot.SetActive(false);
                po.objectName = "";
                onSomething = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                
                if (Input.GetKey(mouseLeft))
                {

                    if (lastMousePos.x != -1f)
                    {
                        Debug.Log("paint");
                        Vector2 offset = mousePosition - lastMousePos;
                        float length = offset.magnitude;
                        offset.Normalize();
                        for (float i = 0; i < length; i += 3)
                        {
                            Vector2 temp = lastMousePos + offset * i;
                            Vector3 objPosition = pCamera.Camera7.ScreenToWorldPoint(new Vector3(temp.x, temp.y, 10));
                            Instantiate(baseDot, objPosition, baseDot.rotation);
                            DrawCircle((int)remap(11.7f, -11.7f, 0, 1024 - 1, objPosition.x), (int)remap(5.77f, -5.77f, 0, 512 - 1, objPosition.y), 5, currentColor);
                            text2d.Apply();

                        }

                    }
                    else
                    {
                        Vector3 objPosition = pCamera.Camera7.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
                        Instantiate(baseDot, objPosition, baseDot.rotation);
                        DrawCircle((int)remap(11.7f, -11.7f, 0, 1024 - 1, objPosition.x), (int)remap(5.77f, -5.77f, 0, 512 - 1, objPosition.y), 5, currentColor);
                        text2d.Apply();
                    }
                    lastMousePos = mousePosition;

                }
                else
                {
                    lastMousePos = new Vector2(-1f, -1f);
                    //text2d.Apply();
                }

            }
        }
           
        else
        {
            Cursor.visible = false;
            pCamera.Camera1.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            chara.m_WalkSpeed = 1 + (mood.happyMeter / 100);
            whiteUselessDot.SetActive(true);
            mood.painting = false;
        }



    }





    void DrawCircle (int cx, int cy, int rad, Color col)
    {
        for(int x = Mathf.Max(cx - rad, 0); x<= Mathf.Min(cx + rad, text2d.width - 1); x++ )
        {
            for(int y = Mathf.Max(cy - rad, 0); y <= Mathf.Min(cy + rad, text2d.height-1); y++ )
            {
                text2d.SetPixel(x, y, col);
            }
        }
    }

    // give a source and destination, start and end, 
    float remap(float src_range_start, float src_range_end, float dst_range_start, float dst_range_end, float value_to_remap)
	{
		return ((dst_range_end - dst_range_start) * (value_to_remap - src_range_start)) / (src_range_end - src_range_start) + dst_range_start;
	}
  

}
