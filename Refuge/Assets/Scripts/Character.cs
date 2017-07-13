using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    string charName;
    string bio;
    public Sprite sprite;
    public Item[] inventory = new Item[4];
    float health;
    float hunger;
    float thirst;
    float stress;
    bool injured;
    bool cholera;
    bool dysentry;
    bool typhoid;
    
    public void AddHealth(float modifier) {health += modifier;}
    public float GetHealth() {return health;}
    public void AddThirst(float modifier) {thirst += modifier; }
    public float GetThirst() {return thirst;}
    public void AddHunger(float modifier) {hunger += modifier;}
    public float GetHunger() {return hunger;}
    public void AddStress(float modifier) {stress += modifier;}
    public float GetStress() {return stress;}
}
