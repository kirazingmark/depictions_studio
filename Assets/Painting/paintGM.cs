using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintGM : MonoBehaviour {

    public Transform baseDot;
    public KeyCode mouseLeft;
    public static string toolType;
    public static float currentScale = 0.05f;

    public Texture2D text2d;

    public GameObject texture1;

    Vector2 lastMousePos;

    //public bool onSomething;

	// Use this for initialization
	void Start () {

        lastMousePos = new Vector2(-1f, -1f);
        text2d = new Texture2D(1024, 512);
        texture1.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", text2d);
        for (int y = 0; y < 512; y++)
            for (int x = 0; x < 1024; x++)
                text2d.SetPixel(x, y, Color.white);
        text2d.Apply();


    }

    // Update is called once per frame
    void Update () {

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
       
        //deleting the whole scene
        if(Input.GetKey(KeyCode.Delete))
        {
            for (int y = 0; y < 512; y++)
                for (int x = 0; x < 1024; x++)
                    text2d.SetPixel(x, y, Color.white);
            text2d.Apply();
        }

        //drawing part
            if (Input.GetKey(mouseLeft))
            {
            if (lastMousePos.x != -1f)
            {
                Vector2 offset = mousePosition - lastMousePos;
                float length = offset.magnitude;
                offset.Normalize();
                for (float i = 0; i < length; i += 5)
                {
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(lastMousePos + offset * i);
                    Instantiate(baseDot, objPosition, baseDot.rotation);
                    DrawCircle((int)remap(-9.3f, 9.3f, 0, 1023, objPosition.x), (int)remap(-5, 5, 0, 511, objPosition.y), 5, Color.black);
                    text2d.Apply();

                }

            }
            else
            {
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Instantiate(baseDot, objPosition, baseDot.rotation);
                DrawCircle((int)remap(-9.3f, 9.3f, 0, 1023, objPosition.x), (int)remap(-5, 5, 0, 511, objPosition.y), 5, Color.black);
                text2d.Apply();
            }
                lastMousePos = mousePosition;
               
            }
            else
            {
                lastMousePos = new Vector2(-1f, -1f);
                text2d.Apply();
            }
        
        
	}

    void DrawPainting(Vector2 position)
    {
        
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
