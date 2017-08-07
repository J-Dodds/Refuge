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

    public int chanceOfNothing = 60;
    public int chanceOfInjury = 70;
    public int chanceOfCholera = 80;
    public int chanceOfDysentary = 90;
    public int chanceOfTyphoid = 100;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
	}
	
	// Update is called once per frame
	void Update () {
        if (newLocation) {
        if (Vector3.Distance(refugeeObj.transform.position, newLocation.transform.position) > 0.6) {
            refugeeObj.transform.position = Vector3.Lerp(refugeeObj.transform.position, newLocation.transform.position, Time.deltaTime * GM.partySpeed);
                foreach (GameObject chara in GM.characters) {
                    chara.GetComponent<Character_r>().AddHunger(-0.002f);
                    chara.GetComponent<Character_r>().AddThirst(-0.002f);

                    int rand = Random.Range(0, 100);
                    if(rand <= chanceOfNothing)
                    {
                        Debug.Log("You caught nothing");
                    }
                    else if(rand <= chanceOfInjury)
                    {
                        chara.GetComponent<Character_r>().injured = true;
                        chara.GetComponent<Character_r>().conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten injured! ");
                    }
                    else if(rand <= chanceOfCholera)
                    {
                        chara.GetComponent<Character_r>().cholera = true;
                        chara.GetComponent<Character_r>().conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten cholera! ");
                    }
                    else if (rand <= chanceOfDysentary)
                    {
                        chara.GetComponent<Character_r>().cholera = true;
                        chara.GetComponent<Character_r>().conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten dysentary! ");
                    }
                    else if(rand <= chanceOfTyphoid)
                    {
                        chara.GetComponent<Character_r>().typhoid = true;
                        chara.GetComponent<Character_r>().conditionReportText.text += (chara.GetComponent<Character_r>().charName + " has gotten typhoid! ");
                    }

                    StartCoroutine(chara.GetComponent<Character_r>().HasGottenHealthCondition());
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

    public void Travel (GameObject location) {
        newLocation = location;
    }
}
