using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    GameManager _GameManager;
    public UIChar _UICharacter;

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
        if (_GameManager.carryingItem) {
            _UICharacter.chara.UseItem(_GameManager.carryingItem.GetComponent<Item>());
            _GameManager.carryingItem = null;
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
        GameObject click = eventData.pointerCurrentRaycast.gameObject;
        if (click == this.gameObject) {
            if (_GameManager.carryingItem) {
                _UICharacter.chara.UseItem(_GameManager.carryingItem.GetComponent<Item>());
                _GameManager.carryingItem = null;
            }
        }
    }
}
