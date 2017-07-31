using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_r : MonoBehaviour {

    [SerializeField]
    string charName, bio;
    [SerializeField]
    public GameObject[] inventory;
    float health, hunger, thirst, stress; // Normalized
    bool injured, cholera, dysentery, typhoid;

    public Sprite sprite;
    [SerializeField]
    GameObject UISprite; // Button
    [SerializeField]
    GameObject UIHealth, UIHunger, UIThirst, UIStress; // Sliders

    public void AddHealth(float modifier) { health += modifier; Mathf.Clamp01(health); UIHealth.GetComponent<Slider>().value = health; }
    public float GetHealth() { return health; }
    public void AddThirst(float modifier) { thirst += modifier; Mathf.Clamp01(thirst); UIThirst.GetComponent<Slider>().value = thirst; }
    public float GetThirst() { return thirst; }
    public void AddHunger(float modifier) { hunger += modifier; Mathf.Clamp01(hunger); UIHunger.GetComponent<Slider>().value = hunger; }
    public float GetHunger() { return hunger; }
    public void AddStress(float modifier) { stress += modifier; Mathf.Clamp01(stress); UIStress.GetComponent<Slider>().value = stress; }
    public float GetStress() { return stress; }

    void UseItem (Item_r item) {
        AddHealth (item.healthMod);
        AddThirst (item.thirstMod);
        AddHunger (item.hungerMod);
        AddStress (item.stressMod);
        if (Random.Range(0f, 1f) > item.injuryChance)
            injured = true;
        if (Random.Range(0f, 1f) > item.choleraChance)
            cholera = true;
        if (Random.Range(0f, 1f) > item.dysenteryChance)
            dysentery = true;
        if (Random.Range(0f, 1f) > item.typhoidChance)
            typhoid = true;
    }

    void AddItem (GameObject item) {
        for (int index = 0; index < inventory.Length; ++index) {
            if (!inventory[index].GetComponent<InventorySlot_r>().item) {
                inventory[index].GetComponent<InventorySlot_r>().item = item;
                inventory[index].GetComponent<Image>().sprite = item.GetComponent<Item_r>().itemSprite;
            }
        }
    }
}
