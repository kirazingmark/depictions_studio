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
    bool paused;

    public FirstPersonController chara;
    CameraSwitcher pCamera;

    // Use this for initialization
    void Start()
    {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        PausePanel.SetActive(false);
        Cursor.visible = false;
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();


    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (!paused && pCamera.Camera1.enabled)
            {
                paused = true;
                Time.timeScale = 0.00000000001f;
                PausePanel.SetActive(true);

                Cursor.visible = true;
                chara.m_MouseLook.lockCursor = false;
                chara.m_MouseLook.m_cursorIsLocked = false;

            }
            else if(paused && pCamera.Camera1.enabled)
            {
                paused = false;
                Time.timeScale = 1;
                PausePanel.SetActive(false);
                //Cursor.visible = false;
                //chara.m_MouseLook.lockCursor = true;
                //chara.m_MouseLook.m_cursorIsLocked = true;

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
