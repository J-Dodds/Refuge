using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItem : MonoBehaviour, IPointerDownHandler, IDropHandler {

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
        OnClick();
    }

    void IDropHandler.OnDrop(PointerEventData eventData) {
        OnClick();
    }

    public void OnClick() {
        if (_GameManager.carryingItem) {
            _UICharacter.chara.UseItem(_GameManager.carryingItem.GetComponent<Item>());
            _GameManager.carryingItem = null;
        }
    }
}
