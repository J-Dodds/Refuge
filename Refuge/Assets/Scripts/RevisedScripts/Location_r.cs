using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location_r : MonoBehaviour {

    public enum LocationType {
        LTClinic = 0,
        LTAbandonedShop = 1,
        LTMarketplace = 2,
        LTIntermediary = 3,
    }

    public LocationType type;
    public GameObject inventoryUI;
    public GameObject[] inventory;
    public GameObject[] possibleItems;
    float encounterChance;
    Dictionary<GameObject, float> itemSpawnChance;
    Dictionary<GameObject, float> itemPrice;
    string description;

    void GenerateInventory() {
        foreach (GameObject slot in inventory) {
            // No Empty RND Slots
            float rnd = Random.Range(0f, 1f);
            GameObject selectedObj = possibleItems[Random.Range(0, possibleItems.Length)];
            foreach (GameObject item in possibleItems)
                if (selectedObj) {
                    if (itemSpawnChance[item] > rnd && itemSpawnChance[item] - rnd > itemSpawnChance[selectedObj] - rnd) {
                        selectedObj = item;
                    }
                }
            slot.GetComponent<InventorySlot_r>().item = selectedObj;
        }
    }

    void Scavenge() {
        inventoryUI.SetActive(true);
    }

    void StartEncounter() {
        GameObject.Find("GameManager").GetComponent<GameManager_r>().ChangeScreen(GameManager_r.ScreenType.STEncounter);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
