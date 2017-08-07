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
    public int cureCost = 0;
    public int costPerCondition = 100;

    public Text healText;
    public Text cureText;

    bool injured = false;
    bool cholera = false;
    bool dysentary = false;
    bool typhoid = false;

	// Use this for initialization
	void Start ()
    {
        healText.text = "Restore Health = $" + healCost;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (character.GetComponent<Character_r>().injured == true)
        {
            if (injured == false)
            {
                cureCost += costPerCondition;
                injured = true;
            }
        }

        if (character.GetComponent<Character_r>().cholera == true)
        {
            if (cholera == false)
            {
                cureCost += costPerCondition;
                cholera = true;
            }
        }

        if (character.GetComponent<Character_r>().dysentery == true)
        {
            if (dysentary == false)
            {
                cureCost += costPerCondition;
                dysentary = true;
            }
        }

        if(character.GetComponent<Character_r>().typhoid == true)
        {
            if (injured == false)
            {
                cureCost += costPerCondition;
                typhoid = true;
            }
        }

        cureText.text = "Cure Disease = $" + cureCost;
    }

    public void HealRefugee()
    {
        if (character.GetComponent<Character_r>().health >= 100)
        {
            if (gameManager.partyMoney >= healCost)
            {
                character.GetComponent<Character_r> ().health += healAmount;
                Debug.Log(character.GetComponent<Character_r>().health);
                gameManager.partyMoney -= healCost;
            }
        }
    }

    public void CureDisease()
    {
        if(character.GetComponent<Character_r>().injured == true || character.GetComponent<Character_r>().cholera == true || character.GetComponent<Character_r>().dysentery == true || character.GetComponent<Character_r>().typhoid == true)
        {
            if(gameManager.partyMoney >= cureCost)
            {
                character.GetComponent<Character_r>().injured = false;
                character.GetComponent<Character_r>().cholera = false;
                character.GetComponent<Character_r>().dysentery = false;
                character.GetComponent<Character_r>().typhoid = false;
                gameManager.partyMoney -= cureCost;

                injured = false;
                cholera = false;
                dysentary = false;
                typhoid = false;
                cureCost = 0;
            }
        }
    }
}
