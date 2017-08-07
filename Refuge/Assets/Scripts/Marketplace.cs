using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    public Text foodText;
    public Text waterText;
    public Text firstAidKitText;

    GameManager_r gameManager;
    void Start()
    {
        foodText.text = "Food = " + foodPrice;
        waterText.text = "Water = " + waterPrice;
        firstAidKitText.text = "First Aid Kit = " + firstAidKitPrice;
    }

    public void AddFoodToInventory()
    {
        if (gameManager.partyMoney >= foodPrice)
        {
            character.GetComponent<Character_r>().AddItem(foodItem);
            gameManager.partyMoney -= foodPrice;
        }
    }

    public void AddWaterToInventory()
    {
        if (gameManager.partyMoney >= waterPrice)
        {
            character.GetComponent<Character_r>().AddItem(waterItem);
            gameManager.partyMoney -= waterPrice;
        }
    }

    public void AddFirstAidKitToInventory()
    {
        if (gameManager.partyMoney >= firstAidKitPrice)
        {
            character.GetComponent<Character_r>().AddItem(firstAidKitItem);
            gameManager.partyMoney -= firstAidKitPrice;
        }
    }
}
