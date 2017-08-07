using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_r : MonoBehaviour {
    
    public enum ScreenType {
        STWorldMap = 0,
        STHubMap = 1,
        STEncounter = 2,
        STLocation = 3,
        STMarket = 4,
        STPause = 5,
        STOptions = 6,
        STCredits = 7,
    };

    public GameObject charUI;
    public GameObject[] characters;
    public GameObject mouseHoverTip;
    public GameObject carryingItem;
    ScreenType currentScreen, prevScreen;
    Dictionary<ScreenType, GameObject> screens = new Dictionary<ScreenType, GameObject>();

    public int partyMoney;
    public float partySpeed = 2;

    float hoverTimer = 0;

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
        // Find each screen object
        screens.Add(ScreenType.STHubMap, GameObject.FindGameObjectWithTag("ScreenHubMap")); // Char UI
        screens.Add(ScreenType.STWorldMap, GameObject.FindGameObjectWithTag("ScreenWorldMap")); // Char UI
        screens.Add(ScreenType.STEncounter, GameObject.FindGameObjectWithTag("ScreenEncounter"));
        screens.Add(ScreenType.STLocation, GameObject.FindGameObjectWithTag("ScreenLocation"));
        screens.Add(ScreenType.STMarket, GameObject.FindGameObjectWithTag("ScreenMarket"));
        screens.Add(ScreenType.STPause, GameObject.FindGameObjectWithTag("ScreenPause"));
        screens.Add(ScreenType.STOptions, GameObject.FindGameObjectWithTag("ScreenOptions"));
        screens.Add(ScreenType.STCredits, GameObject.FindGameObjectWithTag("ScreenCredits"));

        for (int index = 0; index < screens.Count; ++index) 
            if (screens[(ScreenType)index])
                screens[(ScreenType)index].SetActive(false);
        ChangeScreen(ScreenType.STPause);
        foreach (GameObject chara in characters) {
            chara.GetComponent<Character_r>().AddHealth(1);
            chara.GetComponent<Character_r>().AddHunger(1);
            chara.GetComponent<Character_r>().AddThirst(1);
            chara.GetComponent<Character_r>().AddStress(1);
        }
    }

    public void ChangeScreen(ScreenType newScreen) {
        prevScreen = currentScreen;
        screens[currentScreen].SetActive(false);
        screens[newScreen].SetActive(true);
        currentScreen = newScreen;

        // UI Requirements
        if (charUI)
        if (currentScreen == ScreenType.STHubMap || currentScreen == ScreenType.STWorldMap)
            charUI.SetActive(true);
        else
            charUI.SetActive(false);
    }

    public void ChangeScreen(int iNewScreen) {
        ScreenType newScreen = (ScreenType)iNewScreen;
        prevScreen = currentScreen;
        screens[currentScreen].SetActive(false);
        screens[newScreen].SetActive(true);
        currentScreen = newScreen;

        // UI Requirements
        if (charUI)
            if (currentScreen == ScreenType.STHubMap || currentScreen == ScreenType.STWorldMap)
                charUI.SetActive(true);
            else
                charUI.SetActive(false);
    }

    public GameObject WealthiestChar(Item_r.ItemType itemType) {
        GameObject chara = new GameObject();
        int maxItemCount = 0;
        foreach (GameObject character in characters) {
            int itemCount = 0;
            foreach (GameObject item in character.GetComponent<Character_r>().inventory) {
                if (item.GetComponent<Item_r>().itemType == itemType)
                    ++itemCount;
            }
            if (itemCount > maxItemCount) {
                maxItemCount = itemCount;
                chara = character;
            }
        }
        return chara;
    }

    public void AddMoney(int modifier) { partyMoney += modifier; }
    public int GetMoney() { return partyMoney; }
}
