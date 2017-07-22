using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuTransition : MonoBehaviour {
	public GameObject destinationPanel;
	public GameObject thisPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick()
	{
		//Turn on the next scene and off this one
		thisPanel.SetActive(false);
		destinationPanel.SetActive(true);
	}
}
