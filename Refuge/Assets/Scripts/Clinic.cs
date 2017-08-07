using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Clinic : Locations
{
    public int healAmount;
    public Character_r character;
    public GameManager_r gameManager;

    public int healCost = 100;
    public int cureCost = 200;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void HealRefugee()
    {
        if (character.GetComponent<Refugees>().health >= 100)
        {
            if (gameManager.partyMoney >= healCost)
            {
                character.GetComponent<Refugees>().health += healAmount;
                Debug.Log(character.GetComponent<Refugees>().health);
                gameManager.partyMoney -= healCost;
            }
        }
    }

    public void CureDisease()
    {
        if(character.GetComponent<Refugees>().isSick == true)
        {
            if(gameManager.partyMoney >= cureCost)
            {
                character.GetComponent<Refugees>().isSick = false;
                gameManager.partyMoney -= cureCost;

            }
        }
    }
}
