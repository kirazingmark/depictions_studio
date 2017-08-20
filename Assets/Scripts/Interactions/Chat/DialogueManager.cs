using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public DialogueParser parser;

    public string dialogue, characterName;
    public int lineNum;
    int pose;
    string[] options;
    public bool playerTalking;
    List<Button> buttons = new List<Button>();

    public Text dialogueBox;
    public Text nameBox;
    public GameObject choiceBox;

    public GameObject panel;

    Mood mood;

    // Use this for initialization
    void Start()
    {
        dialogue = "";
        characterName = "";
        pose = 0;
        playerTalking = false;
        parser = GameObject.Find("ChatGM").GetComponent<DialogueParser>();
        lineNum = 0;
        //panel.SetActive(false);

        mood = GameObject.FindGameObjectWithTag("Player").GetComponent<Mood>();
    }

    // Update is called once per frame
    void Update()
    {
        // interacting when a trigger is being called
        if (parser.gameStart)
        {
            if (Input.GetMouseButtonDown(0) && playerTalking == false)
            {
                ShowDialogue();

                lineNum++;
            }
        }
        

        UpdateUI();

      
    }

    public void ShowDialogue()
    {
        ParseLine();
    }

    void UpdateUI()
    {
        if (!playerTalking)
        {
            ClearButtons();
        }
        dialogueBox.text = dialogue;
        nameBox.text = characterName;
    }

    void ClearButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            print("Clearing buttons");
            Button b = buttons[i];
            buttons.Remove(b);
            Destroy(b.gameObject);
        }
    }

    void ParseLine()
    {
        if (parser.GetName(lineNum) != "Player")
        {
            playerTalking = false;
            characterName = parser.GetName(lineNum);
            dialogue = parser.GetContent(lineNum);
            pose = parser.GetPose(lineNum);
            mood.happyMeter += (float)pose;
        }
        else
        {
            playerTalking = true;
            characterName = "";
            dialogue = "";
            pose = 0;
            options = parser.GetOptions(lineNum);
            CreateButtons();
        }
    }

    void CreateButtons()
    {
        for (int i = 0; i < options.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(choiceBox);
            Button b = button.GetComponent<Button>();
            ChooseButtons cb = button.GetComponent<ChooseButtons>();
            cb.SetText(options[i].Split(':')[0]);
            cb.option = options[i].Split(':')[1];
            cb.box = this;
            b.transform.SetParent(this.transform);
            b.transform.localPosition = new Vector3(0, -25 + (i * 50)); //changing the distance from the other
            b.transform.localScale = new Vector3(1, 1, 1);
            buttons.Add(b);
        }
    }

    
}
