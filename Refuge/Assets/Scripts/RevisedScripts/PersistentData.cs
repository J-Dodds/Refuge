using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

struct CharPacket {
    public float health;
    public float hunger;
    public float thirst;
    public int[] inventory;
    public bool injured;
    public bool cholera;
    public bool dysentery;
    public bool typhoid;
}

struct DataPacket {
    public string fungusBlock;
    public int currentScreen; // ID
    public int currentArea; // Map ID
    public int currentLoc; // Location ID within Map
    public int totalLoc; // Total number of locations
    public int[][] locationInventory;
    public CharPacket[] chars;
    public int money;
}

public class PersistentData : MonoBehaviour {

    void SaveCSV (DataPacket data) {
        string unwrittenData;
        unwrittenData = string.Format("{0},{1},{2},{3},{4},", data.fungusBlock, data.currentScreen.ToString(), data.currentArea.ToString(), data.currentLoc.ToString(), data.totalLoc.ToString());
        for (int i = 0; i < data.totalLoc; ++i) {

        }
        TextWriter sw = new StreamWriter("");
        string thing1 = "_test_";
        float thing2 = 5.52f;
        sw.WriteLine("{0},{1}", thing1, thing2.ToString());
    }

    DataPacket LoadCSV () {
        DataPacket data = new DataPacket();
        string[] passedData;
        StreamReader sr = new StreamReader("");
        passedData = sr.ReadToEnd().Split(',');
        data.fungusBlock = passedData[0];
        data.currentScreen = int.Parse(passedData[1]);
        data.currentArea = int.Parse(passedData[2]);
        data.currentLoc = int.Parse(passedData[3]);
        data.totalLoc = int.Parse(passedData[4]);
        int currentIndex = 5;
        // Location Inventories
        for (int i = 0; i < data.totalLoc; ++i) {
            for (int a = 0; a < 4; ++a) {
                data.locationInventory[i][a] = int.Parse(passedData[currentIndex]);
                ++currentIndex;
            }
        }
        for (int i = 0; i < 4; ++i) {
            data.chars[i].health = int.Parse(passedData[currentIndex]);
            data.chars[i].hunger = int.Parse(passedData[currentIndex + 1]);
            data.chars[i].thirst = int.Parse(passedData[currentIndex + 2]);
            currentIndex += 3;
            for (int a = 0; a < 4; ++a) {
                data.chars[i].inventory[a] = int.Parse(passedData[currentIndex]);
                ++currentIndex;
            }
            data.chars[i].injured = bool.Parse(passedData[currentIndex]);
            data.chars[i].cholera = bool.Parse(passedData[currentIndex + 1]);
            data.chars[i].dysentery = bool.Parse(passedData[currentIndex + 2]);
            data.chars[i].typhoid = bool.Parse(passedData[currentIndex + 3]);
            currentIndex += 4;
        }
        data.money = int.Parse(passedData[currentIndex]);

        return data;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
