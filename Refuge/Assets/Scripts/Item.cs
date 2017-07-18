using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public enum ItemType {
        itBakedBeans = 0,
        itPeanutButter = 1,
        itTrailMix = 2,
        itCleanWater = 3,
        itDirtyWater = 4,
        itFirstAidKit = 5,
        itMedicine = 6,
        itBooks = 7,
        itHandheldPuzzle = 8,
        itChocolatePudding = 9,
        itRepairFracture = 10,
        itIntravenousHydration = 11,
        itMetronidazole = 12,
        itPenicillin = 13,
    };

    public ItemType itemType;
    public bool carry;
    public Sprite itemSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (FindObjectOfType<GameManager>().carryingItem == gameObject) {

            if (!gameObject.GetComponent<SpriteRenderer>())
                gameObject.AddComponent<SpriteRenderer>();
            gameObject.GetComponent<SpriteRenderer>().sprite = itemSprite;
 
            gameObject.layer = 5;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            GetComponent<SpriteRenderer>().sortingOrder = 5;
            transform.localScale = new Vector3(2,2,1);
        }
        else {
            if (gameObject.GetComponent<SpriteRenderer>())
                Destroy(gameObject.GetComponent<SpriteRenderer>());
            gameObject.layer = 5;
        }
	}
}
