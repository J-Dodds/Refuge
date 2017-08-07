using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_r : MonoBehaviour {

    [SerializeField]
    public string charName, bio;
    [SerializeField]
    public GameObject[] inventory;
    public float health, hunger, thirst, stress; // Normalized
    public bool injured, cholera, dysentery, typhoid;

    public Sprite sprite;
    [SerializeField]
    GameObject UISprite; // Button
    [SerializeField]
    GameObject UIHealth, UIHunger, UIThirst, UIStress; // Sliders

    public void AddHealth(float modifier) { health += modifier; health = Mathf.Clamp01(health); UIHealth.GetComponent<Slider>().value = health; }
    public float GetHealth() { return health; }
    public void AddThirst(float modifier) { thirst += modifier; thirst = Mathf.Clamp01(thirst); UIThirst.GetComponent<Slider>().value = thirst; }
    public float GetThirst() { return thirst; }
    public void AddHunger(float modifier) { hunger += modifier; hunger = Mathf.Clamp01(hunger); UIHunger.GetComponent<Slider>().value = hunger; }
    public float GetHunger() { return hunger; }
    public void AddStress(float modifier) { stress += modifier; stress = Mathf.Clamp01(stress); UIStress.GetComponent<Slider>().value = stress; }
    public float GetStress() { return stress; }

    public void UseItem () {
        GameManager_r GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
        Item_r item = GM.carryingItem.GetComponent<Item_r>();
        GameObject.Find("GameManager").GetComponent<GameManager_r>().carryingItem = null;
        AddHealth (item.healthMod);
        AddThirst (item.thirstMod);
        AddHunger (item.hungerMod);
        AddStress (item.stressMod);
        if (Random.Range(0f, 1f) > item.injuryChance)
        {
            injured = true;
            GM.conditionReportText.text = charName + " has gotten injured! ";
            StartCoroutine(GM.HasGottenHealthCondition());
        }

        if (Random.Range(0f, 1f) > item.choleraChance)
        {
            cholera = true;
            GM.conditionReportText.text = charName + " has gotten cholera! ";
            StartCoroutine(GM.HasGottenHealthCondition());
        }

        if (Random.Range(0f, 1f) > item.dysenteryChance)
        {
            dysentery = true;
            GM.conditionReportText.text = charName + " has gotten dysentery! ";
            StartCoroutine(GM.HasGottenHealthCondition());
        }

        if (Random.Range(0f, 1f) > item.typhoidChance)
        {
            typhoid = true;
            GM.conditionReportText.text = charName + " has gotten typhoid! ";
            StartCoroutine(GM.HasGottenHealthCondition());
        }

            if (item.itemType == Item_r.ItemType.ITCureAll) {
            typhoid = false;
            dysentery = false;
            cholera = false;
            injured = false;
        }
        if (item.itemType == Item_r.ItemType.ITHeal)
            AddHealth(1);
    }

    public void AddItem (GameObject item) {
        for (int index = 0; index < inventory.Length; ++index) {
            if (!inventory[index].GetComponent<InventorySlot_r>().item) {
                inventory[index].GetComponent<InventorySlot_r>().item = item;
                inventory[index].GetComponent<Image>().sprite = item.GetComponent<Item_r>().itemSprite;
            }
        }
    }
}
