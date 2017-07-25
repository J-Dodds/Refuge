using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNavigation : MonoBehaviour
{

    public GameObject[] mapNodes;
    public GameObject refugeeObject;

    public int currentLocation = 0;

	// Use this for initialization
	void Start ()
    {
        //Error Logs
		if (mapNodes.Length < 0)
        {
            Debug.Log("No Nodes added");
        }

        if (refugeeObject == null)
        {
            Debug.Log("No Refugee Object");
        }

        //Set start point
        refugeeObject.transform.position = new Vector3( mapNodes[0].transform.position.x, mapNodes[0].transform.position.y + 20f, mapNodes[0].transform.position.z - 3);
	}

    // Update is called once per frame
    void Update()
    {

    }
}


//Check what nodes current node is connected to. if it is move to the node on a click.