using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    // VARIABLES AND CONSTANTS.
    public GameObject PausePanel;
    public string playMainMenu;
    public string playCredits;

    // Use this for initialization
    void Start()
    {

        PausePanel.SetActive(false);
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
                Cursor.visible = true;
                // Screen.lockCursor = false; // Old, depricated function.
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                PausePanel.SetActive(false);
                Cursor.visible = false;
                Screen.lockCursor = true; // Old, depricated function.
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
	}

    // Return to Main Game.
    public void PlayResume()
    {

        Time.timeScale = 1;
        PausePanel.SetActive(false);
        Cursor.visible = false;
        Screen.lockCursor = true; // Old, depricated function.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Return to Main Menu.
    public void PlayMainMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(playMainMenu);
    }

    // Open Crediits.
    public void PlayCredits()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(playCredits);
    }

    // Quit game function.
    public void QuitGame()
    {

        Application.Quit();
    }
}
