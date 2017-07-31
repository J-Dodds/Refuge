using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_r : MonoBehaviour {

    GameManager_r _GameManager;
    Sprite emtpyInv;

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager_r>();
	}

    public void OnClickInventory(GameObject slot) {
        if (slot.GetComponent<InventorySlot_r>().item && !_GameManager.carryingItem) {
            _GameManager.carryingItem = slot.GetComponent<InventorySlot_r>().item;
            slot.GetComponent<InventorySlot>().item = null;
        }
        else if (!slot.GetComponent<InventorySlot_r>().item && _GameManager.carryingItem) {
            slot.GetComponent<InventorySlot_r>().item = _GameManager.carryingItem;
            _GameManager.carryingItem = null;
        }
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
