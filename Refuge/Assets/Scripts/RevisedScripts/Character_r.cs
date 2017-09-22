using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_r : MonoBehaviour {

    [SerializeField]
    public string charName, bio;
    [SerializeField]
    public GameObject[] inventory;
    public float health, hunger, thirst; // Normalized
    public bool injured, cholera, dysentery, typhoid;
    public bool isDead = false;

    public Sprite sprite;
    [SerializeField]
    GameObject UISprite; // Button
    [SerializeField]
    GameObject UIHealth, UIHunger, UIThirst; // Sliders

    public void AddHealth(float modifier) { health += modifier; health = Mathf.Clamp01(health); UIHealth.GetComponent<Slider>().value = health; }
    public float GetHealth() { return health; }
    public void AddThirst(float modifier) { thirst += modifier; thirst = Mathf.Clamp01(thirst); UIThirst.GetComponent<Slider>().value = thirst; }
    public float GetThirst() { return thirst; }
    public void AddHunger(float modifier) { hunger += modifier; hunger = Mathf.Clamp01(hunger); UIHunger.GetComponent<Slider>().value = hunger; }
    public float GetHunger() { return hunger; }

    public GameObject injurySprite;
    public GameObject choleraSprite;
    public GameObject dysenterySprite;
    public GameObject typhoidSprite;

    void Update()
    {
        if(health <= 0)
        {
            isDead = true;
            OnDeath();
        }
    }

    public void UseItem () {
        GameManager_r GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
        Item_r item = GM.carryingItem.GetComponent<Item_r>();
        AddHealth (item.healthMod);
        AddThirst (item.thirstMod);
        AddHunger (item.hungerMod);
        Destroy(item.gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager_r>().carryingItem = null;

        if (Random.Range(0f, 1f) > item.injuryChance)
        {
            injured = true;
            GM.conditionReportText.text = charName + " has gotten injured! ";
            StartCoroutine(GM.HasGottenHealthCondition());
            injurySprite.SetActive(true);
        }

        if (Random.Range(0f, 1f) > item.choleraChance)
        {
            cholera = true;
            GM.conditionReportText.text = charName + " has gotten cholera! ";
            StartCoroutine(GM.HasGottenHealthCondition());
            choleraSprite.SetActive(true);
        }

        if (Random.Range(0f, 1f) > item.dysenteryChance)
        {
            dysentery = true;
            GM.conditionReportText.text = charName + " has gotten dysentery! ";
            StartCoroutine(GM.HasGottenHealthCondition());
            dysenterySprite.SetActive(true);
        }

        if (Random.Range(0f, 1f) > item.typhoidChance)
        {
            typhoid = true;
            GM.conditionReportText.text = charName + " has gotten typhoid! ";
            StartCoroutine(GM.HasGottenHealthCondition());
            typhoidSprite.SetActive(true);
        }

            if (item.itemType == Item_r.ItemType.ITCureAll)
        {
            typhoid = false;
            dysentery = false;
            cholera = false;
            injured = false;
            injurySprite.SetActive(true);
            choleraSprite.SetActive(true);
            dysenterySprite.SetActive(true);
            typhoidSprite.SetActive(true);
        }

        if (item.itemType == Item_r.ItemType.ITHeal)
            AddHealth(1);
    }

    public void AddItem (GameObject item) {
        for (int index = 0; index < inventory.Length; ++index) {
            if (!inventory[index].GetComponent<InventorySlot_r>().item) {
                inventory[index].GetComponent<InventorySlot_r>().item = item;
                inventory[index].GetComponent<Image>().sprite = item.GetComponent<Item_r>().itemSprite;
            }
        }
    }

    public void OnDeath()
    {
        GameManager_r GM = GameObject.Find("GameManager").GetComponent<GameManager_r>();
        GM.conditionReportText.text = charName + " Has Died!";
        StartCoroutine(GM.HasGottenHealthCondition());
        Debug.Log(charName + " has died");
        Destroy(gameObject);
    }
}
