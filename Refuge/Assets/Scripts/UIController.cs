using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameManager _GameManager;
    public Sprite emptyInv;
    public List<UIChar> uiCharacters = new List<UIChar>();

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int index = 0; index < _GameManager.characters.Length; ++index) {
            uiCharacters[index].GetComponent<UIChar>().chara = _GameManager.characters[index];
            uiCharacters[index].sprite.GetComponent<Image>().sprite = _GameManager.characters[index].sprite;
            for (int a = 0; a < _GameManager.characters[index].inventory.Length; ++a)
                uiCharacters[index].inventory[a].GetComponent<Image>().sprite = _GameManager.characters[index].inventory[a].itemSprite;
            uiCharacters[index].health.GetComponent<Slider>().value = _GameManager.characters[index].GetHealth();
            uiCharacters[index].hunger.GetComponent<Slider>().value = _GameManager.characters[index].GetHunger();
            uiCharacters[index].thirst.GetComponent<Slider>().value = _GameManager.characters[index].GetThirst();
            uiCharacters[index].stress.GetComponent<Slider>().value = _GameManager.characters[index].GetStress();
        }
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
