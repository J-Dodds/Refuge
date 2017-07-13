using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameManager _GameManager;
    public Sprite emptyInv;

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
    public void OnClickInventory(GameObject slot) {
        if (_GameManager.carryingItem) {
            slot.GetComponent<InventorySlot>().item = _GameManager.carryingItem;
            slot.GetComponent<Image>().sprite = _GameManager.carryingItem.GetComponent<Item>().itemSprite;
            _GameManager.carryingItem = null;
        }
        else {
            _GameManager.carryingItem = slot.GetComponent<InventorySlot>().item;
            slot.GetComponent<InventorySlot>().item = null;
            slot.GetComponent<Image>().sprite = emptyInv;
        }
    }
}
