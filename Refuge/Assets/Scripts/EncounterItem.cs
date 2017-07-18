using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterItem : MonoBehaviour {

    public enum ItemType {
        itNaration = 0,
        itSpeech = 1,
        itOutcome = 2,
    };

    public ItemType itemType;
    public List<EncounterItem> nextItem = new List<EncounterItem>();
    public string decisionText;
    public string message;
    public Item itemToGive;
    public int moneyToGive;
    public Sprite sprite;

}
