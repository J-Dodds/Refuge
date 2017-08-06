using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Location_r : MonoBehaviour, IPointerClickHandler {

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
    [SerializeField]
    string description;
    Map_r map;

    void Start() {
        map = GameObject.FindGameObjectWithTag("ScreenWorldMap").GetComponent<Map_r>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        map.Travel(gameObject);
    }

    void GenerateInventory() {
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
        if (Random.Range(0f, 1f) > 0.75f)
            inventory[inventory.Length - 1].GetComponent<InventorySlot_r>().item = null;
    }

    public void Scavenge() {
        GenerateInventory();
        inventoryUI.SetActive(true);
    }

    void StartEncounter() {
        GameObject.Find("GameManager").GetComponent<GameManager_r>().ChangeScreen(GameManager_r.ScreenType.STEncounter);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
