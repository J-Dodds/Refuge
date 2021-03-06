﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Map_r : MonoBehaviour
{
    public GameObject[] locations;
    public GameObject refugeeObj;
    GameObject previousLocation;
    public GameObject newLocation;
    public GameManager_r GM;
    float movementXOffset = 10;
    float movementYOffset = 10;

    [SerializeField]
    public int currentLocationNumber = 0;
    public int chanceOfNothing = 75;
    public int chanceOfInjury = 80;
    public int chanceOfCholera = 85;
    public int chanceOfDysentary = 90;
    public int chanceOfTyphoid = 95;

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
    bool logged = false;
    List<float> hunger = new List<float>();
    List<float> thirst = new List<float>();

    /// <summary>
    /// Confirm what? What is this for? - Jordon
    /// </summary>
    public bool confirm;

    // Use this for initialization
    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
        previousLocation = newLocation;

        hungerPercentLeft = 100f;
        thirstPercentLeft = 100f;
        stressPercentLeft = 100f;
        foreach (GameObject chara in GM.characters) {
            hunger.Add(chara.GetComponent<Character_r>().GetHunger());
            thirst.Add(chara.GetComponent<Character_r>().GetThirst());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmTravel == true && !(confirmTravelPanel && confirmTravelPanel.activeSelf))
        {
            if (newLocation)
            {
                if (Vector3.Distance(refugeeObj.transform.position, newLocation.transform.position) > 0.6f)
                {
                    refugeeObj.transform.position = Vector3.Lerp(refugeeObj.transform.position, newLocation.transform.position, Time.deltaTime * GM.partySpeed);

                    //if (GM.inTutorial == false)
                    {
                        int index = 0;
                        foreach (GameObject chara in GM.characters)
                        {
                            float charHunger = chara.GetComponent<Character_r>().GetHunger();
                            float charThirst = chara.GetComponent<Character_r>().GetThirst();
                            charHunger = Mathf.Lerp(charHunger, hunger[index] - newLocation.GetComponent<Location_r>().distance * 0.25f, Time.deltaTime * 2.5f);
                            charThirst = Mathf.Lerp(charThirst, thirst[index] - newLocation.GetComponent<Location_r>().distance * 0.25f, Time.deltaTime * 2.5f);
                            //if (!logged)
                                //Debug.Log(charHunger);

                            //chara.GetComponent<Character_r>().AddHunger(-1);
                            //chara.GetComponent<Character_r>().AddHunger(charHunger);
                            chara.GetComponent<Character_r>().AddHunger(-1);
                            chara.GetComponent<Character_r>().AddThirst(-1);
                            //chara.GetComponent<Character_r>().AddHunger(Mathf.Lerp(chara.GetComponent<Character_r>().GetHunger(), 1 - newLocation.GetComponent<Location_r>().distance * 0.25f, Time.deltaTime * 3));
                            //chara.GetComponent<Character_r>().AddThirst(Mathf.Lerp(chara.GetComponent<Character_r>().GetThirst(), 1 - newLocation.GetComponent<Location_r>().distance * 0.25f, Time.deltaTime * 3));
                            chara.GetComponent<Character_r>().AddHunger(charHunger);
                            chara.GetComponent<Character_r>().AddThirst(charThirst);
                            //Debug.Log(charHunger);
                            //chara.GetComponent<Character_r>().AddHunger(Mathf.Lerp(charHunger, -newLocation.GetComponent<Location_r>().distance * 0.25f, Time.deltaTime));
                            //chara.GetComponent<Character_r>().AddThirst(Mathf.Lerp(chara.GetComponent<Character_r>().GetThirst(), -newLocation.GetComponent<Location_r>().distance * 0.25f, Time.deltaTime));
                            //Debug.Log(string.Format("Supposed to take {0} off, for some reason we took bloody {1} off", -newLocation.GetComponent<Location_r>().distance * 0.25f, 1 - chara.GetComponent<Character_r>().GetHunger()));
                            /*
                             * Add(LERP(1, -0.75, T)) = Adding 0.25
                             * Add(-1 - LERP(1, -0.75, T) = 
                             * Starting Value = 1
                             * Ending Value = 0.25
                             * Overall Add -0.75
                             * V = D/T
                             * D
                             * VT
                             * D = -0.75
                             * T = 
                             */
                            if (chara.GetComponent<Character_r>().hunger == 0 || chara.GetComponent<Character_r>().thirst == 0)
                            {
                                //chara.GetComponent<Character_r>().AddHealth(-0.005f);
                            }
                            ++index;
                        }
                    }
                }
                else
                {
                    Debug.Log("We Made It! (woo)");
                    for (int i = 0; i < hunger.Count; ++i) {
                        hunger[i] = GM.characters[i].GetComponent<Character_r>().GetHunger();
                        thirst[i] = GM.characters[i].GetComponent<Character_r>().GetThirst();
                    }
                    logged = false;
                    refugeeObj.transform.position = Vector3.MoveTowards(refugeeObj.transform.position, new Vector3(newLocation.transform.position.x + movementXOffset, newLocation.transform.position.y + movementYOffset, -5), 1.0f);
                    newLocation.GetComponent<Location_r>().Scavenge();
                    confirmTravel = false;
                    costOfTravelText.text = "";
                }
            }
        }
    }

    public void Travel(GameObject location)
    {
        //if (newLocation != location || newLocation.GetComponent<Location_r>().possibleLocations.Contains(location))
        if (newLocation != location && location.GetComponent<Location_r>().locationNumber == currentLocationNumber + 1)
        {
            //if (location.GetComponent<Location_r>().locationNumber == currentLocationNumber - 1 || location.GetComponent<Location_r>().locationNumber == currentLocationNumber + 1)
            //{
            time = (int)Vector3.Distance(refugeeObj.transform.position, location.transform.position) / 5;

            newLocation = location;

            if (confirmTravelPanel)
            {
                confirmTravelPanel.SetActive(true);

                //Im aware the numbers don't match with the value they are actually going down by, but this fits the slider much much better - Jordon
                //costOfTravelText.text += "Hunger - " + time * 0.0003f * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) * 1000 + "\n" +
                //                         "Thirst - " + time * 0.0003f * (((newLocation.transform.position.x - refugeeObj.transform.position.x) + (newLocation.transform.position.y - refugeeObj.transform.position.y)) * Time.deltaTime * GM.partySpeed) * 1000 + "\n";
                                         /*"You will lose health if hunger or thirst are empty"*/
                costOfTravelText.text = string.Format("This journey will take {0} days to travel\nThis will deplete {1}% of your total hunger, and {2}% of your total thirst", location.GetComponent<Location_r>().distance, location.GetComponent<Location_r>().distance * 0.25f, location.GetComponent<Location_r>().distance * 0.25f);
            }
            else
            {
                YesTravel();
            }
        }
        else
            if (newLocation == location)
                Debug.Log("newLocation and location are one in the same");
            if (newLocation.GetComponent<Location_r>().locationNumber != currentLocationNumber + 1)
                Debug.Log(location.name);
                //Debug.Log("location numbers are mismatched: " + newLocation.GetComponent<Location_r>().locationNumber + ", " + currentLocationNumber);
            //Debug.Log(location.GetComponent<Location_r>().locationNumber + " | " + currentLocationNumber + " | " + locations.Length);
    }

    public void YesTravel()
    {
        currentLocationNumber = newLocation.GetComponent<Location_r>().locationNumber;

        if (!GM)
            GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();

        if (GM)
        {
            if (GM.inTutorial == false)
            {
                foreach (GameObject chara in GM.characters)
                {
                    int rand = Random.Range(0, 100);
                    //Debug.Log(rand);

                    if (rand <= chanceOfNothing)
                    {
                        Debug.Log("You caught nothing");
                    }
                    else if (rand > chanceOfNothing && rand <= chanceOfInjury)
                    {
                        chara.GetComponent<Character_r>().injured = true;
                        GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten injured! ");
                        StartCoroutine(GM.HasGottenHealthCondition());
                        chara.GetComponent<Character_r>().injurySprite.SetActive(true);
                    }
                    else if (rand > chanceOfInjury && rand <= chanceOfCholera)
                    {
                        chara.GetComponent<Character_r>().cholera = true;
                        GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten cholera! ");
                        StartCoroutine(GM.HasGottenHealthCondition());
                        chara.GetComponent<Character_r>().choleraSprite.SetActive(true);
                    }
                    else if (rand > chanceOfCholera && rand <= chanceOfDysentary)
                    {
                        chara.GetComponent<Character_r>().dysentery = true;
                        GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten dysentary! ");
                        StartCoroutine(GM.HasGottenHealthCondition());
                        chara.GetComponent<Character_r>().dysenterySprite.SetActive(true);
                    }
                    else if (rand > chanceOfDysentary && rand <= chanceOfTyphoid)
                    {
                        chara.GetComponent<Character_r>().typhoid = true;
                        GM.conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten typhoid! ");
                        StartCoroutine(GM.HasGottenHealthCondition());
                        chara.GetComponent<Character_r>().typhoidSprite.SetActive(true);
                    }
                }
            }
            else
            {
                Debug.Log("In tutorial");
            }
                    
        }
        else
        {
            Debug.Log("A thing is broken");
            //StartCoroutine(GM.HasGottenHealthCondition());
        }

        if (newLocation.GetComponent<Location_r>() != null)
        {
            currentLocationNumber = newLocation.GetComponent<Location_r>().locationNumber;
        }
        else if (newLocation.GetComponent<Clinic_r>())
        {
            currentLocationNumber = newLocation.GetComponent<Clinic_r>().locationNumber;
        }

        confirmTravel = true;
        if (confirmTravelPanel)
            confirmTravelPanel.SetActive(false);
    }

    public void NoTravel()
    {
        newLocation = previousLocation;
        confirmTravelPanel.SetActive(false);
        costOfTravelText.text = "";
        Debug.Log("panel shut");
    }
}
