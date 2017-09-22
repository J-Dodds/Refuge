using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbandonedShop : Locations
{
    public int chanceForNothing = 15;
    public int chanceForWater = 34;
    public int chanceForFood = 51;

    public GameObject character;
    public GameObject foodItem;
    public GameObject waterItem;

    public Text resultText;

    bool beenSearched = false;

	// Use this for initialization
	void Start ()
    {
		if(chanceForFood + chanceForNothing + chanceForWater > 100)
        {
            Debug.Log("chance values greater thant 100%");
        }
	}

    public void SearchBuilding()
    {
        int random = Random.Range(1, 100);

        if(beenSearched == false)
        {
            if(random <= chanceForNothing)
            {
                resultText.text = "You found nothing";
                Debug.Log("Recived nothing");
            }
            else if (random > chanceForNothing && random <= chanceForNothing + chanceForWater)
            {
                resultText.text = "You found water!";
                character.GetComponent<Character>().AddItem(waterItem);
            }
            else if (random > chanceForNothing + chanceForWater && random <= chanceForNothing + chanceForWater + chanceForFood)
            {
                resultText.text = "You found food!";
                character.GetComponent<Character>().AddItem(foodItem);
            }
        }
    }
}

//click a button to search the building

//Water
//35%
//Canned Beans
//50%
//Nothing
//15%
