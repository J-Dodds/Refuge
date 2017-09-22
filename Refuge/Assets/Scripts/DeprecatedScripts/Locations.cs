using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locations : MonoBehaviour
{
    //GameObject[] inventory;
    //float encounterChance;
    //Dictionary<int, float> itemSpawnChance; // Will be 100% for each item in clinic
    //Dictionary<int, float> itemPrice; // Will be 0 for scavenged items
    //string description;

    public enum LocationType
    {
        LTempty,
        LTclinic,
        LTabandonedShop,
        LTmarketplace,
            
        LTcount
    }

    public LocationType locationType;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Fills inventory based on the chance of objects spawning(if in abandoned shop or marketplace)
    //Runs when the party enters the location
    void GenerateInventory()
    {

    }

    //Displays inventory, allowing player to drag items to character inventories
    void Scavenge()
    {

    }

    //Change Screen to Encounter Screen through GameManager
    void StartEncounter()
    {

    }
}
