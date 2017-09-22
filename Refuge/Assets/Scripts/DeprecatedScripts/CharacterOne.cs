using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOne : MonoBehaviour
{
    Character_r playerOne;

	// Use this for initialization
	void Start ()
    {
        playerOne = this.gameObject.GetComponent<Character_r>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(playerOne.isDead == true)
        {
            Debug.Log("Game Over Yo");
        }
	}
}
