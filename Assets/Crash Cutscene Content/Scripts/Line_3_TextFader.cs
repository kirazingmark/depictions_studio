using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line_3_TextFader : MonoBehaviour {

    void Awake()
    {
        // Call 'SetTextToZeroAlpha' at runtime.
        StartCoroutine(SetTextToZeroAlpha(0.01f, GetComponent<Text>()));
        StartCoroutine(FadeManager());
    }

    // can ignore the update, it's just to make the coroutines get called for example
    void Update()
    {

    }

    // Fade Text Element to 1 when called.
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    // Fade Text Element Alpha to 0 when called.
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    // Set Text Element Alpha to 0 to begin with.
    public IEnumerator SetTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeManager()
    {
        yield return new WaitForSeconds(16);
        StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
        yield return null;
    }
}