using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_r : MonoBehaviour {

    public enum ItemType {

    };

    public ItemType itemType;
    public Sprite itemSprite;
    public float healthMod, hungerMod, thirstMod, stressMod;
    public float injuryChance, choleraChance, dysenteryChance, typhoidChance;

}
