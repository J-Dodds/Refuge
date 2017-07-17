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
    
    public void AddHealth(float modifier) { health += modifier; uiChar.health.GetComponent<Slider>().value = health; }
    public float GetHealth() { return health; }
    public void AddThirst(float modifier) { thirst += modifier; uiChar.thirst.GetComponent<Slider>().value = thirst; }
    public float GetThirst() { return thirst; }
    public void AddHunger(float modifier) { hunger += modifier; uiChar.hunger.GetComponent<Slider>().value = hunger; }
    public float GetHunger() { return hunger; }
    public void AddStress(float modifier) { stress += modifier; uiChar.stress.GetComponent<Slider>().value = stress; }
    public float GetStress() { return stress; }
}
