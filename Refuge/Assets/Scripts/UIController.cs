using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameManager _GameManager;
    public Sprite emptyInv;
    public List<UIChar> uiCharacters = new List<UIChar>();
    public Text partyMoney;
    public InputField moneyModifier;
    public Button addMoney;

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int index = 0; index < _GameManager.characters.Length; ++index) {
            uiCharacters[index].GetComponent<UIChar>().chara = _GameManager.characters[index];
            uiCharacters[index].sprite.GetComponent<Image>().sprite = _GameManager.characters[index].sprite;
            for (int a = 0; a < _GameManager.characters[index].inventory.Length; ++a) {
                if (_GameManager.characters[index].inventory[a]) {
                    uiCharacters[index].inventory[a].GetComponent<Image>().sprite = _GameManager.characters[index].inventory[a].itemSprite;
                }
            }
            _GameManager.characters[index].AddHealth(1f);
            _GameManager.characters[index].AddHunger(0.75f);
            _GameManager.characters[index].AddThirst(0.25f);
            _GameManager.characters[index].AddStress(0.15f);
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
        // Update all slots
        for (int index = 0; index < uiCharacters.Count; ++index) {
            for (int a = 0; a < uiCharacters[index].inventory.Length; ++a) {
                if (uiCharacters[index].inventory[a].GetComponent<InventorySlot>().item) {
                    uiCharacters[index].chara.inventory[a] = uiCharacters[index].inventory[a].GetComponent<InventorySlot>().item.GetComponent<Item>();
                }
                else
                    uiCharacters[index].chara.inventory[a] = null;
            }
        }
    }

    public void OnClickMoney() {
        int modifier;
        if(int.TryParse(moneyModifier.text, out modifier))
            _GameManager.AddMoney(modifier);
        else
            Debug.Log("Please enter a valid value");
        partyMoney.text = "Cash: " + _GameManager.GetMoney().ToString();
    }

    public void ChangeSFXVolume(float vol) {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().GetChannel("SFX").volume = vol;
    }

    public void ChangeMusicVolume(float vol) {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().GetChannel("Music").volume = vol;
    }

    public void ChangeAmbientVolume(float vol) {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().GetChannel("Ambient").volume = vol;
    }

    public void ChangeMasterVolume(float vol) {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().masterVolume = vol;
    }
}
