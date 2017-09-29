using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;  //Need to use this to get different scenes because Unity has taken out LoadLevel.

public class MainMenu : MonoBehaviour {
	

	public string playGameLevel;

	public string playMenuLevel;



	//Play Game Function.
	public void PlayGame() {
		//Need this function to open up a different scene

		SceneManager.LoadScene (playGameLevel);

	}

	//Quit game function.
	public void QuitGame() {
		
		Application.Quit ();


	}

    public void Start() {
		        //Set Cursor to not be visible
        Cursor.visible = true;
        
    }

	public void MainMenus() {

		SceneManager.LoadScene ("MainMenu");


	}
}
