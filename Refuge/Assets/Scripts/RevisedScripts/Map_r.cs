using System.Collections;
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

    // Use this for initialization
    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
    }

    // Update is called once per frame
    void Update() {
        if (newLocation)
        {
            if (Vector3.Distance(refugeeObj.transform.position, newLocation.transform.position) > 0.6)
            {
                refugeeObj.transform.position = Vector3.Lerp(refugeeObj.transform.position, newLocation.transform.position, Time.deltaTime * GM.partySpeed);

                foreach (GameObject chara in GM.characters)
                {
                    float hungerPercentLeft = chara.GetComponent<Character_r>().hunger / 1 * 100;
                    float thirstPercentLeft = chara.GetComponent<Character_r>().thirst / 1 * 100;
                    float stressPercentLeft = chara.GetComponent<Character_r>().stress / 1 * 100;

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
                newLocation = null;
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
            Debug.Log(location.GetComponent<Location_r>().locationNumber + " | " + currentLocationNumber +  " | " + locations.Length);
    }
}
