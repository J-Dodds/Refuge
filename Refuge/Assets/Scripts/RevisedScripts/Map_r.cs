using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_r : MonoBehaviour {

    public GameObject[] locations;
    public GameObject refugeeObj;
    GameObject newLocation;
    GameManager_r GM;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(refugeeObj.transform.position, newLocation.transform.position) > 0.05) {
            refugeeObj.transform.position = Vector3.Lerp(refugeeObj.transform.position, newLocation.transform.position, Time.deltaTime);
            foreach (GameObject chara in GM.characters) {
                chara.GetComponent<Character_r>().AddHunger(-0.01f);
                chara.GetComponent<Character_r>().AddThirst(-0.01f);
            }
        }
	}

    void Travel (GameObject location) {
        newLocation = location;
    }
}
