using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOpenCloseJournal : MonoBehaviour {
    public GameObject journal;
    public Toggle toggle;
    public static UIOpenCloseJournal instance;

        private void Awake()
    {
        instance = this;
    }

    public bool IsJournalOpen
    {
        get
        {
            //toggle.isOn = IsJournalOpen;
            return IsJournalOpen;
        }
        set
        {
            if (value == false)
            {
                journal.SetActive(false);
                toggle.isOn = false;
            }
            if (value == true)
            {
                journal.SetActive(true);
                toggle.isOn = true;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
