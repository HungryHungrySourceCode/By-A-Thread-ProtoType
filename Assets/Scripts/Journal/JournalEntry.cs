using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntry : MonoBehaviour {
    public int JournalID;

    public string date;
    public string title;

    public bool textOrImage;

    public int fontSize = 18;

    [TextArea(2, 30)]
    public string text;

    public Texture2D image;

    public bool hasBeenRead = false;



    public DialogueBox dialogueBox;
    public Button thisButton;
    public Text dateText;
    public Text titleText;


    public void OnMouseClick()
    {
        dialogueBox.BreakDialogueAndDisplay(text, fontSize);
        dialogueBox.scrollField.dragValue = 1f;
    }

    [ExecuteInEditMode]
	void Start () {
        dialogueBox = Journal.journal.mainDialogueBox;
        dateText.text = date;
        titleText.text = title;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
