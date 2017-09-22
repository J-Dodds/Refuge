using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour {

    public EncounterItem currentItem;
    public List<Button> decButtons = new List<Button>();
    public Text encounterText;
    public InventorySlot slot;
    public Sprite image;
    public Text partyMoney;

    GameManager _GameManager;

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && currentItem.nextItem.Count == 1)
            NextItem();
	}

    public void NextItem(int Encounter = 0) {
        
        if (currentItem.nextItem[0]) {
            currentItem = currentItem.nextItem[Encounter];
            encounterText.text = currentItem.message;
            for (int index = 0; index < decButtons.Count; ++index) {
                decButtons[index].gameObject.SetActive(false);
                if (index < currentItem.nextItem.Count) {
                    decButtons[index].gameObject.SetActive(true);
                    decButtons[index].GetComponentInChildren<Text>().text = currentItem.nextItem[index].decisionText;
                }
            }

            _GameManager.AddMoney(currentItem.moneyToGive);
            partyMoney.text = "Cash: " + _GameManager.GetMoney().ToString();

            slot.gameObject.SetActive(false);
            if (currentItem.itemToGive) {
                slot.gameObject.SetActive(true);
                slot.item = currentItem.itemToGive.gameObject;
                slot.GetComponent<Image>().sprite = slot.item.GetComponent<Item>().itemSprite;
            }

            if (currentItem.nextItem.Count > 4) {
                Debug.Log("Too Many Encounter Items!");
            }

            if (image && currentItem.sprite)
               image = currentItem.sprite;
            }
    }

    void DecisionButtonClick(int ID) {
        NextItem(ID);
    }
}
