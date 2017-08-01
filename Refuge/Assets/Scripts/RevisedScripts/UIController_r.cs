﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController_r : MonoBehaviour {

    GameManager_r _GameManager;
    public Sprite emtpyInv;

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager_r>();
	}

    public void OnClickInventory(GameObject slot) {
        if (slot.GetComponent<InventorySlot_r>().item && !_GameManager.carryingItem) {
            _GameManager.carryingItem = slot.GetComponent<InventorySlot_r>().item;
            slot.GetComponent<Image>().sprite = emtpyInv;
            slot.GetComponent<InventorySlot_r>().item = null;
        }
        else if (!slot.GetComponent<InventorySlot_r>().item && _GameManager.carryingItem) {
            slot.GetComponent<InventorySlot_r>().item = _GameManager.carryingItem;
            slot.GetComponent<Image>().sprite = slot.GetComponent<InventorySlot_r>().item.GetComponent<Item_r>().itemSprite;
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