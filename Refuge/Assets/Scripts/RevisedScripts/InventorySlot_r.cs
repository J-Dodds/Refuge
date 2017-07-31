using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot_r : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IDropHandler {

    public GameObject item;
    GameObject character;
    GameManager_r _GameManager;
    UIController_r _UIController;

	// Use this for initialization
	void Start () {
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager_r>();
        _UIController = GameObject.Find("UIController").GetComponent<UIController_r>();
	}
	
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
        _UIController.OnClickInventory(gameObject);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
        _UIController.OnClickInventory(gameObject);
    }

    void IDragHandler.OnDrag(PointerEventData eventData) {
        _UIController.OnClickInventory(gameObject);
    }

    void IDropHandler.OnDrop(PointerEventData eventData) {
        _UIController.OnClickInventory(gameObject);
    }

}
