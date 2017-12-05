using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseManager : MonoBehaviour {

    // VARIABLES AND CONSTANTS.
    public GameObject PausePanel;
    public string playMainMenu;
    public string playCredits;

    public FirstPersonController chara;


    // Use this for initialization
    void Start()
    {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        PausePanel.SetActive(false);
        //Cursor.visible = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(chara.m_MouseLook.m_cursorIsLocked);
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
                Cursor.visible = true;
                chara.m_MouseLook.lockCursor = false;
                chara.m_MouseLook.m_cursorIsLocked = false;
                //Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                PausePanel.SetActive(false);
                Cursor.visible = false;
                chara.m_MouseLook.lockCursor = true;
                chara.m_MouseLook.m_cursorIsLocked = true;
                //Screen.lockCursor = true; // Old, depricated function.
                //Cursor.lockState = CursorLockMode.Locked;
            }
        }
	}

    // Return to Main Game.
    public void PlayResume()
    {

        Time.timeScale = 1;
        PausePanel.SetActive(false);
        Cursor.visible = false;
        chara.m_MouseLook.m_cursorIsLocked = true;
        //Screen.lockCursor = true; // Old, depricated function.
        //Cursor.lockState = CursorLockMode.Locked;
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
