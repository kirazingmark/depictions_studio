using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    // CONSTANTS & VARIABLES.
    public string levelToLoad;

    // Use this for initialization
    void Start () {

        Cursor.visible = false;
        StartCoroutine(ChangeScene());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(59);

        MainMenu();

        yield return null;
    }

    public void MainMenu()
    {

        SceneManager.LoadScene(levelToLoad);
    }
}
