using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Clinic : Locations
{
    public int healAmount;
    public GameObject refugees;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void HealRefugee()
    {
        refugees.GetComponent<Refugees>().health += healAmount;
        Debug.Log(refugees.GetComponent<Refugees>().health);
    }
}
