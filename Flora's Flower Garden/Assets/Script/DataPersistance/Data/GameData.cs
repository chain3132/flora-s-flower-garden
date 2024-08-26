using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerLevel;
    public float currentExp;
    public int currentCoin;
    public List<SoilInPot> soilInPot = new List<SoilInPot>();
    public List<FlowerPotData> flowerInPots = new List<FlowerPotData>();
    public List<Item> Items;
    public Item flowerItem;
    public float _sfxVolume;
    public float _musicVolume;
    
    
    public GameData()
    {
        this.playerLevel = 1;
        this.currentExp = 0f;
        this.currentCoin = 0;
    
    }
}
[System.Serializable]
public class FlowerPotData
{
    public string id;
    public string flowerName;
    public int plantAge;
    public int currentStage;
    public bool potHasSeed;
    public bool watered;
    public int[] growStages;
    public Sprite[] flowerSprites;
    public bool flowerMaxGrow;
}

[System.Serializable]
public class SoilInPot
{
    public string id;
    public bool potHasSoil;
}

[System.Serializable]
public class ItemsData
{
    public int id;
    public string itemName;
    public Sprite icon;
    public bool canSell;
    public int price = 0;
}