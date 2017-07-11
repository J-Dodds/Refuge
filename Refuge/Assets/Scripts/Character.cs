using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    string name;
    string bio;
    public Item[] inventory = new Item[4];
    float health;
    float hunger;
    float thirst;
    float stress;
    bool injured;
    bool cholera;
    bool dysentry;
    bool typhoid;

}
