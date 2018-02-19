using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour {
    public bool isJournalOpen;
    Animator uiAnimator;
    public static Journal journal;
    public GameObject journalGameObject;
    public GameObject journalListPanel;
    public DialogueBox mainDialogueBox;

    private void Awake()
    {
        journal = this;
    }

    public void OpenJournal()
    {
        isJournalOpen = true;
        uiAnimator.SetBool("Open", true);    
    }
    
    public void CloseJournal()
    {
        isJournalOpen = false;
        uiAnimator.SetBool("Open", false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
