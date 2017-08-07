using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Location_r : MonoBehaviour, IPointerClickHandler {

    public bool changeScreen = false;
    public int destinationScreen;

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
    public int locationNumber = 0;
    float encounterChance;
    public string description;
    public Map_r map;

    public void Start() {
        map = GameObject.FindGameObjectWithTag("ScreenWorldMap").GetComponent<Map_r>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        map.Travel(gameObject);
    }

    public virtual void GenerateInventory() {
        foreach (GameObject slot in inventory) {
            // No Empty RND Slots
            float rnd = Random.Range(0f, 1f);
            GameObject selectedObj = possibleItems[Random.Range(0, possibleItems.Length)];
            foreach (GameObject item in possibleItems)
                if (selectedObj) {
                    if (item.GetComponent<Item_r>().spawnChance > rnd && item.GetComponent<Item_r>().spawnChance - rnd > selectedObj.GetComponent<Item_r>().spawnChance - rnd) {
                        selectedObj = item;
                    }
                }
            slot.GetComponent<InventorySlot_r>().item = selectedObj;
            slot.GetComponent<Image>().sprite = selectedObj.GetComponent<Item_r>().itemSprite;
        }
        if (Random.Range(0f, 1f) > 0.75f) {
            inventory[inventory.Length - 1].GetComponent<InventorySlot_r>().item = null;
            inventory[inventory.Length - 1].GetComponent<Image>().sprite = GameObject.Find("UIController").GetComponent<UIController_r>().emtpyInv;
        }

        if(changeScreen)
        {
            GameManager_r.Instance.ChangeScreen(destinationScreen); // THIS BREAKS THE FUNCTION FOR SOME REASON
        }
    }

    public void Scavenge() {
        GenerateInventory();
        inventoryUI.SetActive(true);
    }

    public void StartEncounter() {
        GameObject.Find("GameManager").GetComponent<GameManager_r>().ChangeScreen(GameManager_r.ScreenType.STEncounter);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
