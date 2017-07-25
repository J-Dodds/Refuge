using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChar : MonoBehaviour {

    public GameObject sprite;
    public GameObject[] inventory;
    public GameObject health;
    public GameObject hunger;
    public GameObject thirst;
    public GameObject stress;
    public Character chara;
    GameManager _GameManager;

    private void Start() {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Refresh() {
        sprite.GetComponent<Image>().sprite = chara.sprite;
        for (int i = 0; i < chara.inventory.Length; ++i) {
            if (chara.inventory[i])
                inventory[i].GetComponent<Image>().sprite = chara.inventory[i].itemSprite;
            else
                inventory[i].GetComponent<Image>().sprite = _GameManager.defaultInventorySprite;
        }
        health.GetComponent<Slider>().value = chara.GetHealth();
        hunger.GetComponent<Slider>().value = chara.GetHunger();
        thirst.GetComponent<Slider>().value = chara.GetThirst();
        stress.GetComponent<Slider>().value = chara.GetStress();

    }
}
