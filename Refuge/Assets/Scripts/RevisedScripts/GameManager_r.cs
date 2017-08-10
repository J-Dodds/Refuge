using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        STClinic = 8,
    };

    public GameObject charUI;
    public GameObject[] characters;
    public GameObject mouseHoverTip;
    public GameObject carryingItem;
    public GameObject moneyGUI;
    public Text conditionReportText;
    public float reportActiveTime = 1f;
    ScreenType currentScreen, prevScreen;
    Dictionary<ScreenType, GameObject> screens = new Dictionary<ScreenType, GameObject>();

    public int partyMoney;
    public float partySpeed = 2;

    float hoverTimer = 0;
   public bool inCoRoutine = false;

    // Singleton
    public static GameManager _Instance;
    public static GameManager Instance {
        get {
            if (_Instance == null)
                _Instance = new GameManager();
            return _Instance;
        }
    }

    public IEnumerator HasGottenHealthCondition()
    {
        inCoRoutine = true;
        conditionReportText.gameObject.SetActive(true);
        yield return new WaitForSeconds(reportActiveTime);
        conditionReportText.text = "";
        conditionReportText.gameObject.SetActive(false);
        inCoRoutine = false;
        StopCoroutine(HasGottenHealthCondition());
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
        screens.Add(ScreenType.STClinic, GameObject.FindGameObjectWithTag("ScreenClinic"));

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

    void Update() {
        if (carryingItem) {
            if (!carryingItem.activeInHierarchy) {
                if (!carryingItem.GetComponent<Image>()) {
                    carryingItem = Instantiate(carryingItem);
                    carryingItem.AddComponent<Image>();
                    carryingItem.GetComponent<Image>().sprite = carryingItem.GetComponent<Item_r>().itemSprite;
                    carryingItem.transform.SetParent(GameObject.Find("Canvas").transform);
                    carryingItem.gameObject.transform.SetSiblingIndex(carryingItem.gameObject.transform.GetSiblingIndex());
                    carryingItem.GetComponent<Image>().raycastTarget = false;
                }
            }
            carryingItem.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5);
        }
        
    }

    public void ChangeScreen(ScreenType newScreen) {
        prevScreen = currentScreen;
        screens[currentScreen].SetActive(false);
        screens[newScreen].SetActive(true);
        currentScreen = newScreen;
        mouseHoverTip.SetActive(false);

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
		mouseHoverTip.SetActive(false);

        // UI Requirements
        if (charUI)
            if (currentScreen == ScreenType.STHubMap || currentScreen == ScreenType.STWorldMap || currentScreen == ScreenType.STClinic || currentScreen == ScreenType.STMarket)
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

    public void AddMoney(int modifier) { partyMoney += modifier; if (moneyGUI) moneyGUI.GetComponent<Text>().text = "Money: " + partyMoney; }
    public int GetMoney() { return partyMoney; }
}
