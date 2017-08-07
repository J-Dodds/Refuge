using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clinic_r : Location_r, IPointerClickHandler {

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        map.Travel(gameObject);
    }

    public override void GenerateInventory() {
        int diseaseCount = 0;
        foreach (GameObject chara in GameObject.Find("GameManager").GetComponent<GameManager_r>().characters) {
            if (chara.GetComponent<Character_r>().injured)
                ++diseaseCount;
            if (chara.GetComponent<Character_r>().dysentery)
                ++diseaseCount;
            if (chara.GetComponent<Character_r>().cholera)
                ++diseaseCount;
            if (chara.GetComponent<Character_r>().typhoid)
                ++diseaseCount;
        }
    }

}
