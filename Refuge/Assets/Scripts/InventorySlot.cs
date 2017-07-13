using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    public GameObject item;
    GameManager _GameManager;

	// Use this for initialization
	void Start () {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
        if (!_GameManager.clicking) {
            GameObject.Find("UIController").GetComponent<UIController>().OnClickInventory(this.gameObject);
            _GameManager.lastClickTime = Time.time;
            _GameManager.clicking = true;
        }
    }



    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
        GameObject slot = eventData.pointerCurrentRaycast.gameObject;
        //Debug.Log(eventData.clickTime - lastClickTime);
        if (slot.GetComponent<InventorySlot>() && Time.time > _GameManager.lastClickTime + 0.2f) {
            GameObject.Find("UIController").GetComponent<UIController>().OnClickInventory(slot);
            _GameManager.clicking = false;
        }
    }

}
