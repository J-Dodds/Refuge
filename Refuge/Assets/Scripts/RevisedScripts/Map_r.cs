﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Map_r : MonoBehaviour {

    public GameObject[] locations;
    public GameObject refugeeObj;
    GameObject newLocation;
    GameManager_r GM;
    float movementXOffset = 10;
    float movementYOffset = 10;

    [SerializeField]
    int currentLocationNumber;
    public int chanceOfNothing = 60;
    public int chanceOfInjury = 70;
    public int chanceOfCholera = 80;
    public int chanceOfDysentary = 90;
    public int chanceOfTyphoid = 100;

    public GameObject confirmTravelPanel;
    [SerializeField]
    Text costOfTravelText;
    bool? confirmTravel = null;
    public GameObject locationObject;

    float hungerPercentLeft;
    float thirstPercentLeft;
    float stressPercentLeft;

    // Use this for initialization
    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();

        hungerPercentLeft = 100f;
        thirstPercentLeft = 100f;
        stressPercentLeft = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if(confirmTravelPanel.activeInHierarchy == true)
        {
            newLocation = locationObject;
            costOfTravelText.text = "You Will Lose: Health - " + -(((100.0f - hungerPercentLeft) / 1000000.0f) + ((100f - thirstPercentLeft) / 1000000f) + ((100f - stressPercentLeft) / 1000000f)) * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) + "\n" +
                                "                   Hunger - " + 0.002f * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) + "\n" +
                                "                   Thirst - " + 0.002f * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) + "\n" +
                                "                   Stress - " + 0.0005f * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) + "\n";
        }

        if (confirmTravel == true)
        {
            confirmTravelPanel.SetActive(false);
            if (Vector3.Distance(refugeeObj.transform.position, newLocation.transform.position) > 0.8)
            {
                refugeeObj.transform.position = Vector3.Lerp(refugeeObj.transform.position, newLocation.transform.position, Time.deltaTime * GM.partySpeed);

                foreach (GameObject chara in GM.characters)
                {
                    chara.GetComponent<Character_r>().AddHunger(-0.002f);
                    chara.GetComponent<Character_r>().AddThirst(-0.002f);
                    chara.GetComponent<Character_r>().AddStress(-0.0005f);

                    chara.GetComponent<Character_r>().AddHealth(-(((100.0f - hungerPercentLeft) / 100000.0f) + ((100f - thirstPercentLeft) / 100000f) + ((100f - stressPercentLeft) / 100000f)));
                    Debug.Log("Heal Loss == " + -(((100.0f - hungerPercentLeft) / 1000000.0f) + ((100f - thirstPercentLeft) / 1000000f) + ((100f - stressPercentLeft) / 1000000f)));
                }
            }
            else {
                Debug.Log("We Made It! (woo)");
                refugeeObj.transform.position = new Vector3(newLocation.transform.position.x + movementXOffset, newLocation.transform.position.y + movementYOffset, -5);
                newLocation.GetComponent<Location_r>().Scavenge();
                confirmTravel = false;
            }
        }
    }

    public void Travel(GameObject location)
    {
        if (location.GetComponent<Location_r>().locationNumber == currentLocationNumber - 1 || location.GetComponent<Location_r>().locationNumber == currentLocationNumber + 1)
        {
            newLocation = location;
            currentLocationNumber = location.GetComponent<Location_r>().locationNumber;
            foreach (GameObject chara in GM.characters)
            {
                int rand = Random.Range(0, 100);
                if (rand <= chanceOfNothing)
                {
                    Debug.Log("You caught nothing");
                }
                else if (rand > chanceOfNothing && rand <= chanceOfInjury)
                {
                    chara.GetComponent<Character_r>().injured = true;
                    GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten injured! ");
                }
                else if (rand > chanceOfInjury && rand <= chanceOfCholera)
                {
                    chara.GetComponent<Character_r>().cholera = true;
                    GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten cholera! ");
                }
                else if (rand > chanceOfCholera && rand <= chanceOfDysentary)
                {
                    chara.GetComponent<Character_r>().dysentery = true;
                    GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten dysentary! ");
                }
                else if (rand > chanceOfDysentary && rand <= chanceOfTyphoid)
                {
                    chara.GetComponent<Character_r>().typhoid = true;
                    GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten typhoid! ");
                }

            }
            StartCoroutine(GM.HasGottenHealthCondition());

        }
        else
            Debug.Log(location.GetComponent<Location_r>().locationNumber + " | " + currentLocationNumber + " | " + locations.Length);
    }

    public void YesTravel()
    {
        confirmTravel = true;
        confirmTravelPanel.SetActive(false);
    }

    public void NoTravel()
    {
        confirmTravel = false;
        confirmTravelPanel.SetActive(false);
    }
}
