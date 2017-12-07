using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip;
    public Text text;

    public IEnumerator playDialogue()
    {
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
        text.gameObject.SetActive(true);

        yield return new WaitForSeconds(audioClip.length);
        text.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        //put text here
        //text.text = "DUMBs";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //calling the function to play the dialogue
    public void PlayDialogue()
    {
        StartCoroutine(playDialogue());
    }
}
