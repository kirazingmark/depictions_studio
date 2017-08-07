using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_GameManager : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public AudioSource introAudioSource;
    public AudioClip introAudioTrack;

    public string levelToLoad;

    // Use this for initialization
    void Start () {

        Cursor.visible = false;
        StartCoroutine(DialogueManager());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DialogueManager() {
        yield return new WaitForSeconds(2);
        introAudioSource.Play();
        yield return new WaitForSeconds(10); // This will need to be adjusted for the whole length of the track.
        PlayGame();
        yield return null;
    }

    //Play Game Function.
    public void PlayGame() {

        SceneManager.LoadScene(levelToLoad);
    }
}
