using UnityEngine;
using System.Collections;
// using UnityEditor;
using System.Text;
using UnityEngine.SceneManagement;
// using UnityEditor.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueParser : MonoBehaviour
{
    public bool character;
    public bool gameStart;

    public CameraSwitcher pCamera;

    public GameObject panel;

    public GameObject player;

    public Button georgiaButton;
    public Button tomButton;

    struct DialogueLine
    {
        public string name;
        public string content;
        public int pose;
        public string[] options;

        public DialogueLine(string Name, string Content, int Pose/*, string Position*/)
        {
            name = Name;
            content = Content;
            pose = Pose;
            //position = Position;
            options = new string[0];
        }
    }

    List<DialogueLine> lines;

    // Use this for initialization
    void Start()
    {
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pCamera.Camera5.enabled = false;
            pCamera.Camera1.enabled = true;
            //player.transform.position = new Vector3(-46, 2, 10);
        }
        if (pCamera.Camera5.enabled == true)
        {
            //unlock mouse
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            panel.SetActive(true);

            gameStart = true;
            if (character)
            {
                string file = "Assets/Scripts/Interactions/Chat/Data/Dialogue";
                string sceneNum = SceneManager.GetActiveScene().name;
                sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
                file += sceneNum;
                file += ".txt";

                lines = new List<DialogueLine>();

                LoadDialogue(file);
            }
            else
            {
                string file2 = "Assets/Scripts/Interactions/Chat/Data/Dialogue2";
                string sceneNum = SceneManager.GetActiveScene().name;
                sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
                file2 += sceneNum;
                file2 += ".txt";

                lines = new List<DialogueLine>();

                LoadDialogue(file2);
            }
        }
        else
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            pCamera.Camera1.enabled = true;
            panel.SetActive(false);
            gameStart = false;
        }
    }

    void LoadDialogue(string filename)
    {
        string line;
        StreamReader r = new StreamReader(filename);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {
                    string[] lineData = line.Split(';');
                    if (lineData[0] == "Player")
                    {
                        DialogueLine lineEntry = new DialogueLine(lineData[0], "", 0/*, ""*/);
                        lineEntry.options = new string[lineData.Length - 1];
                        for (int i = 1; i < lineData.Length; i++)
                        {
                            lineEntry.options[i - 1] = lineData[i];
                        }
                        lines.Add(lineEntry);
                    }
                    else
                    {
                        DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], int.Parse(lineData[2])/*, lineData[3]*/);
                        lines.Add(lineEntry);
                    }
                }
            }
            while (line != null);
            r.Close();
        }
    }

    //public string GetPosition(int lineNumber)
    //{
    //    if (lineNumber < lines.Count)
    //    {
    //        return lines[lineNumber].position;
    //    }
    //    return "";
    //}

    public string GetName(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].name;
        }
        return "";
    }

    public string GetContent(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].content;
        }
        return "";
    }

    public int GetPose(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].pose;
        }
        return 0;
    }

    public string[] GetOptions(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].options;
        }
        return new string[0];
    }
}