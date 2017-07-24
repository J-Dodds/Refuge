using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marketplace : Locations
{
    public GameObject character;
    public GameObject marketplaceUI;

    public GameObject foodItem;
    public GameObject waterItem;
    public GameObject firstAidKitItem;

    public int foodPrice = 10;
    public int waterPrice = 10;
    public int firstAidKitPrice = 150;


    public void AddFoodToInventory()
    {
        character.GetComponent<Character>().AddItem(foodItem);
    }

    public void AddWaterToInventory()
    {
        character.GetComponent<Character>().AddItem(waterItem);
    }

    public void AddFirstAidKitToInventory()
    {
        character.GetComponent<Character>().AddItem(firstAidKitItem);
    }
}
