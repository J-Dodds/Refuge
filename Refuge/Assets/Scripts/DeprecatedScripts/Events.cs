using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public int eventChance = 50;
    public int numberOfEvents = 10;
    public bool isMoving = false;

    int eventNumber = 0;

	// Use this for initialization
	void Start ()
    {

	}

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            int eventRoll = Random.Range(1, 100);
            Debug.Log("No event: " + eventRoll);

            if (eventRoll <= eventChance)
            {
                eventNumber = Random.Range(1, numberOfEvents);
                PlayEvent(eventNumber);
                isMoving = false;
            }

        }
    }

    void PlayEvent(int eventNo)
    {
        Debug.Log("Playing scene number " + eventNo + ".");

        //SceneManager.LoadScene(eventNo);
    }
}
