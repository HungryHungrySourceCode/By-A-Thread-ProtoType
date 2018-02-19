using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPickup : MonoBehaviour {
    public JournalEntry journalPrefab;
    public Transform player = GameMood.instance.player;
    public bool pickedUp = false;

    public void Update()
    {
    

        if ((player.position - transform.position).magnitude <= 5f && pickedUp == false)
        {
            pickedUp = true;
            PickupAndDisplay();
            Destroy(gameObject);
        }
    }

    public void PickupAndDisplay()
    {
        UIOpenCloseJournal.instance.IsJournalOpen = true;
        JournalEntry journal = Instantiate(journalPrefab, Journal.journal.journalListPanel.transform);
        journal.OnMouseClick();
        return;
        //journalPrefab.GetComponent<JournalEntry>().OnMouseClick();
    }
}
