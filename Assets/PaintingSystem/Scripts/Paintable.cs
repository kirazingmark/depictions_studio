using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour 
{
	public Texture2D BaseCanvasTexture;
	public MeshFilter CanvasMeshFilter;
	public int PixelsPerUnit = 128;
	public float RaycastDistance = 10f;

	public float BrushScale = 0.1f;
	public Texture2D ActiveBrush;

	public Color BrushColour = Color.magenta;
	public Color CanvasDefaultColor = Color.white;
	public Color StencilColour = Color.white;

    CameraSwitcher pCamera;
    ColorSelector colSelect;

	protected Texture2D ActiveCanvasTexture;
	protected Texture2D CanvasShadowTexture;
	protected Rect[] TextureUVs;
	protected Texture2D ActiveStencil;

    

	// Use this for initialization
	void Start () 
	{
		// create the base version of the paintable texture
		int textureWidth = (int)(CanvasMeshFilter.mesh.bounds.size.x * PixelsPerUnit * transform.localScale.x);
		int textureHeight = (int)(CanvasMeshFilter.mesh.bounds.size.y * PixelsPerUnit * transform.localScale.y);
		Texture2D PaintableTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);

		// fill with a default colour
		for (int x = 0; x < PaintableTexture.width; ++x)
		{
			for (int y = 0; y < PaintableTexture.height; ++y)
			{
				PaintableTexture.SetPixel(x, y, CanvasDefaultColor);
			}
		}
		PaintableTexture.Apply();

		// setup our texture atlas
		ActiveCanvasTexture = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
		TextureUVs = ActiveCanvasTexture.PackTextures(new Texture2D[]{PaintableTexture, BaseCanvasTexture}, 1, 2048, false);

		// Duplicate the active canvas texture
		CanvasShadowTexture = new Texture2D(ActiveCanvasTexture.width, ActiveCanvasTexture.height, TextureFormat.ARGB32, false);
		Graphics.CopyTexture(ActiveCanvasTexture, CanvasShadowTexture);

		// calculate the new UV coordinates
		Vector2[] meshUVs = new Vector2[CanvasMeshFilter.mesh.vertexCount];
		
		// Front
		meshUVs[0] = new Vector2(TextureUVs[0].xMin, TextureUVs[0].yMin);
		meshUVs[1] = new Vector2(TextureUVs[0].xMax, TextureUVs[0].yMin);
		meshUVs[2] = new Vector2(TextureUVs[0].xMin, TextureUVs[0].yMax);
		meshUVs[3] = new Vector2(TextureUVs[0].xMax, TextureUVs[0].yMax);
		// Top
		meshUVs[8] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMin);
		meshUVs[9] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMin);
		meshUVs[4] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMax);
		meshUVs[5] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMax);		
		// Back
		meshUVs[10] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMin);
		meshUVs[11] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMin);
		meshUVs[6] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMax);
		meshUVs[7] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMax);		
		// Bottom
		meshUVs[12] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMin);
		meshUVs[14] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMin);
		meshUVs[15] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMax);
		meshUVs[13] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMax);		
		// Left
		meshUVs[16] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMin);
		meshUVs[18] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMin);
		meshUVs[19] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMax);
		meshUVs[17] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMax);		
		// Right        
		meshUVs[20] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMin);
		meshUVs[22] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMin);
		meshUVs[23] = new Vector2(TextureUVs[1].xMin, TextureUVs[1].yMax);
		meshUVs[21] = new Vector2(TextureUVs[1].xMax, TextureUVs[1].yMax);

		// set the new UV coordinates and texture
		CanvasMeshFilter.mesh.uv = meshUVs;
		gameObject.GetComponent<MeshRenderer>().material.mainTexture = ActiveCanvasTexture;

        //getting the camera 
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
        colSelect = GameObject.FindGameObjectWithTag("colorWheel").GetComponent<ColorSelector>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // is the painting part started?
        if(pCamera.Camera7.enabled && Time.timeScale == 1)
        {
            BrushColour = colSelect.finalColor;
            // is the mouse down?
            if (Input.GetMouseButton(0))
            {
                // raycast from the cursor location into the world
                RaycastHit hitInfo;
                Ray mouseRay = pCamera.Camera7.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay, out hitInfo, RaycastDistance))
                {
                    Debug.Log(hitInfo.collider.gameObject);

                    // did we hit the canvas?
                    if (hitInfo.transform.gameObject == gameObject)
                    {
                        PerformPainting(hitInfo.normal, hitInfo.textureCoord);
                        
                    }
                }
            }
        }
	}

	public void ClearStencil()
	{
		SetStencil(null);
	}

	public void SetStencil(Texture2D texture)
	{
		// reset the active texture
		Graphics.CopyTexture(CanvasShadowTexture, ActiveCanvasTexture);
		ActiveCanvasTexture.Apply();

		// update the stencil
		ActiveStencil = texture;

		// nothing more to do if there is no texture
		if (ActiveStencil == null)
			return;

		// calculate the destination bounds
		int targetMinX = (int)(TextureUVs[0].xMin * ActiveCanvasTexture.width);
		int targetMinY = (int)(TextureUVs[0].yMin * ActiveCanvasTexture.height);
		int targetMaxX = (int)(TextureUVs[0].xMax * ActiveCanvasTexture.width);
		int targetMaxY = (int)(TextureUVs[0].yMax * ActiveCanvasTexture.height);

		// apply the stencil
		for (int x = targetMinX; x < targetMaxX; ++x)
		{
			for (int y = targetMinY; y < targetMaxY; ++y)
			{
				// sample the stencil texture
				float uvX = (float)(x - targetMinX)/(float)(targetMaxX - targetMinX);
				float uvY = (float)(y - targetMinY)/(float)(targetMaxY - targetMinY);

				// only apply if the alpha is above the threshold
				if (ActiveStencil.GetPixelBilinear(uvX, uvY).a > 0.5f)
					ActiveCanvasTexture.SetPixel(x, y, StencilColour);
			}
		}
		ActiveCanvasTexture.Apply();
	}

	public void SetBrush(Texture2D newBrush)
	{
		ActiveBrush = newBrush;
	}

	public void ClearCanvas()
	{
		// calculate the canvas bounds
		int targetMinX = (int)(TextureUVs[0].xMin * ActiveCanvasTexture.width);
		int targetMinY = (int)(TextureUVs[0].yMin * ActiveCanvasTexture.height);
		int targetMaxX = (int)(TextureUVs[0].xMax * ActiveCanvasTexture.width);
		int targetMaxY = (int)(TextureUVs[0].yMax * ActiveCanvasTexture.height);

		// apply the stencil
		for (int x = targetMinX; x < targetMaxX; ++x)
		{
			for (int y = targetMinY; y < targetMaxY; ++y)
			{
				ActiveCanvasTexture.SetPixel(x, y, CanvasDefaultColor);
			}
		}
		ActiveCanvasTexture.Apply();

		// update the shadow canvas
		Graphics.CopyTexture(ActiveCanvasTexture, CanvasShadowTexture);
		CanvasShadowTexture.Apply();

		// re-apply the stencil if needed
		SetStencil(ActiveStencil);
	}

	void PerformPainting(Vector3 surfaceNormal, Vector2 uvCoordinate)
	{
        // ignore if the normal is not the correct one
        if (Vector3.Dot(surfaceNormal, transform.forward) < 0.99f)
			return;

		// no brush?
		if (ActiveBrush == null)
			return;


		// rescale the UV coordinate
		Vector2 rescaledUV = new Vector2(uvCoordinate.x * TextureUVs[0].width, uvCoordinate.y * TextureUVs[0].height) + TextureUVs[0].min;

		// convert to pixel coordinates
		int pixelX = (int) (rescaledUV.x * ActiveCanvasTexture.width);
		int pixelY = (int) (rescaledUV.y * ActiveCanvasTexture.height);		

		// calculate number of pixels to change based on scale and brush size
		int numPixelsX = (int)(ActiveBrush.width * BrushScale);
		int numPixelsY = (int)(ActiveBrush.height * BrushScale);

		// calculate the canvas bounds
		int targetMinX = (int)(TextureUVs[0].xMin * ActiveCanvasTexture.width);
		int targetMinY = (int)(TextureUVs[0].yMin * ActiveCanvasTexture.height);
		int targetMaxX = (int)(TextureUVs[0].xMax * ActiveCanvasTexture.width);
		int targetMaxY = (int)(TextureUVs[0].yMax * ActiveCanvasTexture.height);

		// apply the brush
		for (int x = 0; x < numPixelsX; ++x)
		{
			int canvasX = pixelX + x - (numPixelsX / 2);

			for (int y = 0; y < numPixelsY; ++y)
			{
				int canvasY = pixelY + y - (numPixelsY / 2);

				// check if this is a location we are not allowed to change.
				// if it is not then the colour will be different in active and shadow textures. this only works if not painting white
				if (ActiveCanvasTexture.GetPixel(canvasX, canvasY) != CanvasShadowTexture.GetPixel(canvasX, canvasY))
					continue;
				
				// less accurate (but works with white) fallback check
				float stencilUVX = (float)(canvasX - targetMinX)/(float)(targetMaxX - targetMinX);
				float stencilUVY = (float)(canvasY - targetMinY)/(float)(targetMaxY - targetMinY);
				if (ActiveStencil != null && ActiveStencil.GetPixelBilinear(stencilUVX, stencilUVY).a > 0.5f)
					continue;

				// calculate the brush UV
				float uvX = (float)x / (float)numPixelsX;
				float uvY = (float)y / (float)numPixelsY;

				// get the brush value
				Color brushPixel = ActiveBrush.GetPixelBilinear(uvX, uvY);
				Color canvasPixel = CanvasShadowTexture.GetPixel(canvasX, canvasY);

				canvasPixel = Color.Lerp(canvasPixel, BrushColour, brushPixel.r);

				// update the canvases
				ActiveCanvasTexture.SetPixel(canvasX, canvasY, canvasPixel);
				CanvasShadowTexture.SetPixel(canvasX, canvasY, canvasPixel);
			}
		}

		// apply the texture updates
		ActiveCanvasTexture.Apply();
		CanvasShadowTexture.Apply();

	}
}
