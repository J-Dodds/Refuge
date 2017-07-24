using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    string charName;
    string bio;
    public Sprite sprite;
    public UIChar uiChar;
    public Item[] inventory = new Item[4];
    float health;
    float hunger;
    float thirst;
    float stress;
    bool injured;
    bool cholera;
    bool dysentry;
    bool typhoid;
    
    public void AddHealth(float modifier) { health += modifier; Mathf.Clamp01(health); uiChar.health.GetComponent<Slider>().value = health; }
    public float GetHealth() { return health; }
    public void AddThirst(float modifier) { thirst += modifier; Mathf.Clamp01(thirst); uiChar.thirst.GetComponent<Slider>().value = thirst; }
    public float GetThirst() { return thirst; }
    public void AddHunger(float modifier) { hunger += modifier; Mathf.Clamp01(hunger); uiChar.hunger.GetComponent<Slider>().value = hunger; }
    public float GetHunger() { return hunger; }
    public void AddStress(float modifier) { stress += modifier; Mathf.Clamp01(stress); uiChar.stress.GetComponent<Slider>().value = stress; }
    public float GetStress() { return stress; }

    public void UseItem(Item item) {
        switch (item.itemType) {
            case Item.ItemType.itBakedBeans:
                AddHunger(0.4f);
                break;
            case Item.ItemType.itMealRation:
                AddHunger(0.75f);
                break;
            case Item.ItemType.itTrailMix:
                AddHunger(0.25f);
                break;
            case Item.ItemType.itCleanWater:
                AddThirst(0.75f);
                break;
            case Item.ItemType.itDirtyWater:
                if (Random.Range(0f, 1f) > 0.1f)
                    cholera = true;
                if (Random.Range(0f, 1f) > 0.05f)
                    dysentry = true;
                AddThirst(0.5f);
                break;
            case Item.ItemType.itBooks:
                AddStress(-0.5f);
                break;
            case Item.ItemType.itHandheldPuzzle:
                AddStress(-0.5f);
                break;
            case Item.ItemType.itChocolatePudding:
                AddStress(-0.5f);
                break;
            case Item.ItemType.itFirstAid:
                AddHealth(1);
                break;
            case Item.ItemType.itMedicine:
                typhoid = false;
                break;
        }
    }
}
