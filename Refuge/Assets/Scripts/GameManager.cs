using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum ScreenType {
        stWorldMap = 0,
        stHubMap = 1,
        stEncounter = 2,
        stLocation = 3,
        stBarter = 4,
        stPause = 5,
        stOptions = 6,
        stCredits = 7,
    };

    Character[] characters;
    ScreenType currentScreen = ScreenType.stHubMap;
    public Dictionary<ScreenType, GameObject> screens = new Dictionary<ScreenType, GameObject>();
    GameObject charUI;
    public GameObject carryingItem;
    public float lastClickTime;
    public bool clicking = false;

    // Use this for initialization
    void Start () {
		//screens.Add(ScreenType.stHubMap, GameObject.FindGameObjectWithTag("ScreenHubMap"));
        screens.Add(ScreenType.stHubMap, GameObject.FindGameObjectWithTag("ScreenHubMap"));
        screens.Add(ScreenType.stWorldMap, GameObject.FindGameObjectWithTag("ScreenWorldMap"));
        screens.Add(ScreenType.stEncounter, GameObject.FindGameObjectWithTag("ScreenEncounter"));
        screens.Add(ScreenType.stLocation, GameObject.FindGameObjectWithTag("ScreenLocation"));
        screens.Add(ScreenType.stBarter, GameObject.FindGameObjectWithTag("ScreenBarter"));
        screens.Add(ScreenType.stPause, GameObject.FindGameObjectWithTag("ScreenPause"));
        screens.Add(ScreenType.stOptions, GameObject.FindGameObjectWithTag("ScreenOptions"));
        screens.Add(ScreenType.stCredits, GameObject.FindGameObjectWithTag("ScreenCredits"));
        charUI = GameObject.Find("CharacterUI");

        for (int index = 0; index < screens.Count; ++index)
            screens[(ScreenType)index].SetActive(false);
        screens[ScreenType.stHubMap].SetActive(true);

        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
            ChangeScreen((ScreenType)Random.Range(0, 7));
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {

        }
	}

    Character WealthiestChar(Item.ItemType itemType) {
        Character chara = new Character();
        int maxItemCount = 0;
        foreach (Character character in characters) {
            int itemCount = 0;
            foreach (Item item in character.inventory) {
                if (item.itemType == itemType)
                    ++itemCount;
            }
            if (itemCount > maxItemCount) {
                maxItemCount = itemCount;
                chara = character;
            }
        }

        return chara;
    }

    void ChangeScreen(ScreenType newScreen) {
        screens[currentScreen].SetActive(false);
        screens[newScreen].SetActive(true);
        currentScreen = newScreen;

        if (newScreen == ScreenType.stHubMap || newScreen == ScreenType.stEncounter || newScreen == ScreenType.stLocation || newScreen == ScreenType.stWorldMap)
            charUI.SetActive(true);
        else
            charUI.SetActive(false);

    }
}
