﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Map_r : MonoBehaviour {

    public GameObject[] locations;
    public GameObject refugeeObj;
    GameObject previousLocation;
    public GameObject newLocation;
    public GameManager_r GM;
    float movementXOffset = 10;
    float movementYOffset = 10;

    [SerializeField]
    public int currentLocationNumber = 0;
    public int chanceOfNothing = 60;
    public int chanceOfInjury = 70;
    public int chanceOfCholera = 80;
    public int chanceOfDysentary = 90;
    public int chanceOfTyphoid = 100;

    public GameObject confirmTravelPanel;
    [SerializeField]
    Text costOfTravelText;
    bool? confirmTravel = null;
    bool travelReady = false;

    float hungerPercentLeft;
    float thirstPercentLeft;
    float stressPercentLeft;

    GameObject currentLocation;
    int time = 0;

    public bool confirm;

    // Use this for initialization
    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
        previousLocation = newLocation;

        hungerPercentLeft = 100f;
        thirstPercentLeft = 100f;
        stressPercentLeft = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmTravel == true)
        {
            //if(newLocation)
            //{
            if (Vector3.Distance(refugeeObj.transform.position, newLocation.transform.position) > 0.6f)
            {
                refugeeObj.transform.position = Vector3.Lerp(refugeeObj.transform.position, newLocation.transform.position, Time.deltaTime * GM.partySpeed);

                foreach (GameObject chara in GM.characters)
                {
                    chara.GetComponent<Character_r>().AddHunger(-0.0001f * time);
                    chara.GetComponent<Character_r>().AddThirst(-0.0001f * time);

                    if(chara.GetComponent<Character_r>().hunger == 0 || chara.GetComponent<Character_r>().thirst == 0)
                    {
                        chara.GetComponent<Character_r>().AddHealth(-0.005f);

                        costOfTravelText.text += "You Will Lose: Health - " + (-0.005f * 100) + "\n";
                    }
                    else
                    {
                        costOfTravelText.text += "You Will Lose: Health - " + 0 + "\n";
                    }
                }
            }
            else
            {
                Debug.Log("We Made It! (woo)");
                refugeeObj.transform.position = Vector3.MoveTowards(refugeeObj.transform.position, new Vector3(newLocation.transform.position.x + movementXOffset, newLocation.transform.position.y + movementYOffset, -5), 1.0f);
                newLocation.GetComponent<Location_r>().Scavenge();
                confirmTravel = false;
                costOfTravelText.text = "";
            }
        }
    }

    public void Travel(GameObject location)
    {
        if (newLocation != location || newLocation.GetComponent<Location_r>().possibleLocations.Contains(location)) {
            //if (location.GetComponent<Location_r>().locationNumber == currentLocationNumber - 1 || location.GetComponent<Location_r>().locationNumber == currentLocationNumber + 1)
            //{
            time = (int)Vector3.Distance(refugeeObj.transform.position, location.transform.position) / 5;

            newLocation = location;
            if (confirmTravelPanel)
            {
                confirmTravelPanel.SetActive(true);

                    //Im aware the numbers don't match with the value they are actually going down by, but this fits the slider much much better - Jordon
                costOfTravelText.text += "Hunger - " + time * 0.0003f * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) * 1000 + "\n" +
                                         "Thirst - " + time * 0.0003f * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) * 1000 + "\n" +
                                         "You will lose health if hunger or thirst are empty";
            }
            else
            {
                YesTravel();
            }
        }
        else
            Debug.Log(location.GetComponent<Location_r>().locationNumber + " | " + currentLocationNumber + " | " + locations.Length);
    }

    public void YesTravel()
    {
        if (!GM)
            GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
        if (GM) {
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
        }
        else
            Debug.Log("A thing is broken");
        //StartCoroutine(GM.HasGottenHealthCondition());

        if (newLocation.GetComponent<Location_r>() != null)
        {
            currentLocationNumber = newLocation.GetComponent<Location_r>().locationNumber;
        }
        else if(newLocation.GetComponent<Clinic_r>())
        {
            currentLocationNumber = newLocation.GetComponent < Clinic_r>().locationNumber;
        }
        confirmTravel = true;
        if (confirmTravelPanel)
            confirmTravelPanel.SetActive(false);
    }

    public void NoTravel()
    {
        newLocation = previousLocation;
        confirmTravelPanel.SetActive(false);
        Debug.Log("panel shut");
    }
}
