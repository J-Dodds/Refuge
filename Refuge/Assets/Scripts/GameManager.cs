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

    public Character[] characters;
    public ScreenType currentScreen = ScreenType.stPause;
    ScreenType prevScreen;
    public Dictionary<ScreenType, GameObject> screens = new Dictionary<ScreenType, GameObject>();
    GameObject charUI;
    public GameObject levelScripting;
    public GameObject carryingItem;
    public float lastClickTime;
    public bool clicking = false;
    public int partyMoney;
    public Sprite defaultInventorySprite;

        // Singleton
    public static GameManager _Instance;
    public static GameManager Instance {
        get {
            if (_Instance == null)
                _Instance = new GameManager();
            return _Instance;
        }
    }

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
        screens[currentScreen].SetActive(true);
        ChangeScreen(ScreenType.stPause);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.P)) {
            if (currentScreen == ScreenType.stPause) {
                Time.timeScale = 1;
                ChangeScreen(prevScreen);
            }
            else {
                ChangeScreen(ScreenType.stPause);
                Time.timeScale = 0;
            }
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

    public void AddMoney(int modifier) {
        partyMoney += modifier;
    }

    public int GetMoney() {
        return partyMoney;
    }

    public void ChangeScreen(ScreenType newScreen) {
        prevScreen = currentScreen;
        screens[currentScreen].SetActive(false);
        screens[newScreen].SetActive(true);
        currentScreen = newScreen;

        if (newScreen == ScreenType.stHubMap || newScreen == ScreenType.stEncounter || newScreen == ScreenType.stLocation || newScreen == ScreenType.stWorldMap)
            charUI.SetActive(true);
        else
            charUI.SetActive(false);
        if (newScreen == ScreenType.stPause) {
            levelScripting.SetActive(false);
        }
        else
            levelScripting.SetActive(true);
    }

    public void ChangeScreen(int iNewScreen) {
        prevScreen = currentScreen;
        ScreenType newScreen = (ScreenType)iNewScreen;
        screens[currentScreen].SetActive(false);
        screens[newScreen].SetActive(true);
        currentScreen = newScreen;

        if (newScreen == ScreenType.stHubMap || newScreen == ScreenType.stEncounter || newScreen == ScreenType.stLocation || newScreen == ScreenType.stWorldMap)
            charUI.SetActive(true);
        else
            charUI.SetActive(false);

        if (newScreen == ScreenType.stPause) {
            levelScripting.SetActive(false);
        }
        else
            levelScripting.SetActive(true);
    }
}
