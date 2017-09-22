using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public int nodeNumber = -1;
    public MapNavigation mapNavigation;

    public GameObject clinicUI;
    public GameObject marketUI;
    public GameObject abandonedShopUI;

    public float yOffset = 20.0f;

    bool isMoving = false;

	// Use this for initialization
	void Start ()
    {
        //Error logs
		if(nodeNumber == -1)
        {
            Debug.Log("A node number has not been set");
        }

        if(nodeNumber == 0)
        {
            Debug.Log("Node number too low. Start numbering at 1");
        }
	}

    void OnMouseOver()
    {
        //Change sprite
    }

    void OnMouseDown()
    {
        int difference = nodeNumber - mapNavigation.currentLocation;

        if (difference == 2)
        {

            mapNavigation.currentLocation = GetComponent<Nodes>().nodeNumber - 1;
            mapNavigation.refugeeObject.transform.position = new Vector3 (mapNavigation.mapNodes[GetComponent<Nodes>().nodeNumber - 1].transform.position.x, mapNavigation.mapNodes[GetComponent<Nodes>().nodeNumber - 1].transform.position.y + yOffset, -3);

            //What type of node is it?
            if (GetComponent<Locations>().locationType == Locations.LocationType.LTclinic)
            {
                clinicUI.SetActive(true);
                marketUI.SetActive(false);
                abandonedShopUI.SetActive(false);
            }
            else if(GetComponent<Locations>().locationType == Locations.LocationType.LTmarketplace)
            {
                marketUI.SetActive(true);
                clinicUI.SetActive(false);
                abandonedShopUI.SetActive(false);
            }
            else if (GetComponent<Locations>().locationType == Locations.LocationType.LTabandonedShop)
            {
                clinicUI.SetActive(false);
                marketUI.SetActive(false);
                abandonedShopUI.SetActive(true);
            }

            if (GetComponent<Locations>().locationType == Locations.LocationType.LTempty)
            {
                clinicUI.SetActive(false);
                marketUI.SetActive(false);
                abandonedShopUI.SetActive(false);
            }
        }

        //Will lerp this in future, and limit to which nodes a refugee can travel
    }
}
