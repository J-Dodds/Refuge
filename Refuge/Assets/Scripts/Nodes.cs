using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public int nodeNumber = -1;
    public MapNavigation mapNavigation;

	// Use this for initialization
	void Start ()
    {
        //Error logs
		if(nodeNumber == -1)
        {
            Debug.Log("A node number has not been set");
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
        mapNavigation.refugeeObject.transform.position = mapNavigation.mapNodes[nodeNumber - 1].transform.position;

        //Will lerp this in future, and limit to which nodes a refugee can travel
    }
}
