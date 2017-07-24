using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    // VARIABLES AND CONSTANTS.
    public GameObject pausePanel;
    public bool isPaused;
    public string playMainMenu;

    // Use this for initialization
    public void Start () {

        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("p"))
        {
            if (isPaused == false)
            {
                OnPause();
                Time.timeScale = 0;
                Cursor.visible = true;
            }
            else if (isPaused == true)
            {
                OnUnPause();
                Time.timeScale = 1;
                Cursor.visible = false;
            }
        }
    }

    public void OnPause() {

        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void OnUnPause() {

        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    // Return to Main Menu.
    public void PlayMainMenu() {

        SceneManager.LoadScene(playMainMenu);
    }

    // Quit game function.
    public void QuitGame() {

        Application.Quit();
    }
}
