using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingText : MonoBehaviour {

    public Text text;

    public IEnumerator StartingSubtitle()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        text.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartingSubtitle());

    }

    // Update is called once per frame
    void Update()
    {

    }
}
