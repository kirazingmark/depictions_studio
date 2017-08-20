using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class ChooseChat : MonoBehaviour {
    DialogueParser parser;
    public List<Button> buttons = new List<Button>();

    // Use this for initialization
    void Start()
    {
        parser = GameObject.Find("ChatGM").GetComponent<DialogueParser>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadChat1()
    {
        parser.gameStart = true;
        parser.character = true;
        ClearButtons();

    }
    public void LoadChat2()
    {
        parser.gameStart = true;
        parser.character = false;
        ClearButtons();
    }

    void ClearButtons()
    {
        for (int i = 0; i < 2; i++)
        {
            print("Clearing buttons");
            Button b = buttons[i];
            Destroy(b.gameObject);
        }
    }


}
