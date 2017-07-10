using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public int nodeNumber = -1;
    public MapNavigation mapNavigation;
    public Events events;

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
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseOver()
    {
        //Change sprite
    }

    void OnMouseDown()
    {
        //Move to node on button click
        events.isMoving = true;
        mapNavigation.currentLocation = GetComponent<Nodes>().nodeNumber - 1;
        mapNavigation.refugeeObject.transform.position = mapNavigation.mapNodes[GetComponent<Nodes>().nodeNumber - 1].transform.position;
        //Will lerp this in future, and limit to which nodes a refugee can travel
    }
}
