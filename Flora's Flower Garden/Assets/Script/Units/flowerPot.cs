using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class flowerPot : MonoBehaviour,IDataPersistance
{
    
    
    private GameObject _potThatSelected;
    private Sprite[] flowerSprite;
    private Item flowerStore;
    
    [Header("Flower")]
    public SpriteRenderer _spriteRenderer;
    public int[] _growstage;
    public int _currentStage = 0;
    public string flowerName;
    public int plantAge;
    public bool potHasSeed = false;
    private int timeChange ;
    public bool watered = false;
    public bool maxFlowerGrow;

    [Header("CoinExpGenerate")] 
    public GameObject[] Prefab;//add coin and exp prefab
    public int CoinsPerTime = 10; // Maximum number of coins per day
    private int currentCoinCount = 0;
    public float coinProductionInterval = 0.3f;
    private Coroutine coinProductionCoroutine;
    private Coroutine RandomDropCoroutine;
    private float maxDropInterval = 10f;
    private float minDropInterval = 5f;
    
    
    [Header("GenerateId")]
    [SerializeField] private string id;
    
    
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        plantAge = 0;
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDayPass += DayPass;
        }

        
        RandomDropCoroutine = StartCoroutine(RandomDropCoins()); // When this script is loaded, it will start randomly dropping coins as long as their is seed in the pot.
        
    }

    
    public void LoadData(GameData data)
    {
        flowerStore = data.flowerItem;
        
        foreach (var potData in data.flowerInPots)
        {
            
            if (potData.id == id) // check Is the loaded potData has the same id as pot id. if not ,it will check next potData until it finds one with the same id as pot id.
            {
                //Set the variable to the saved data.
                flowerName = potData.flowerName;
                plantAge = potData.plantAge;
                _currentStage = potData.currentStage;
                potHasSeed = potData.potHasSeed;
                watered = potData.watered;
                _growstage = potData.growStages;
                maxFlowerGrow = potData.flowerMaxGrow;
                
                // Load the saved flower sprites if this pot has seeds in the saved data.
                if (potHasSeed)
                {
                    flowerSprite = new Sprite[potData.flowerSprites.Length];
                    for (int i = 0; i < potData.flowerSprites.Length; i++)
                    {
                        flowerSprite[i] = potData.flowerSprites[i];
                    }
                    _spriteRenderer.sprite = flowerSprite[_currentStage - 1];// set sprite flower as the saved sprite flower.
                }
                
                break;
            }
        }
    }

    public void SaveData( GameData data)
    {
        data.flowerItem = flowerStore;
        if (data.flowerInPots == null)
        {
            data.flowerInPots = new List<FlowerPotData>();
        }

        // Find the existing entry
        FlowerPotData existingPotData = data.flowerInPots.Find(potData => potData.id == id);

        if (existingPotData != null)
        {
            // Update the existing entry
            
            existingPotData.flowerName = flowerName;
            existingPotData.plantAge = plantAge;
            existingPotData.currentStage = _currentStage;
            existingPotData.potHasSeed = potHasSeed;
            existingPotData.watered = watered;
            existingPotData.growStages = _growstage;
            existingPotData.flowerMaxGrow = maxFlowerGrow;

            // Update flower sprites
            if (potHasSeed)
            {
                existingPotData.flowerSprites = new Sprite[flowerSprite.Length];
                for (int i = 0; i < flowerSprite.Length; i++)
                {
                    existingPotData.flowerSprites[i] = flowerSprite[i];
                }
            }
        }
        else // if there are no any save data,create one .
        {
            FlowerPotData potData = new FlowerPotData();
            potData.id = id; 
            potData.flowerName = flowerName;
            potData.plantAge = plantAge;
            potData.currentStage = _currentStage;
            potData.potHasSeed = potHasSeed;
            potData.watered = watered;
            potData.growStages = _growstage;
            potData.flowerMaxGrow = maxFlowerGrow;
    
            // Save flower sprites
            if (potHasSeed)
            {
                potData.flowerSprites = new Sprite[flowerSprite.Length];
                for (int i = 0; i < flowerSprite.Length; i++)
                {
                    potData.flowerSprites[i] = flowerSprite[i];
                }
            }

            data.flowerInPots.Add(potData); // put data to data.flowerInPots.
        }
        
        
    }
    /// <summary>
    /// random time countdown to start drop coins when call this function
    /// so It will drop coins at different times.
    /// </summary>
    /// <returns></returns>
    IEnumerator RandomDropCoins()
    {
        while (true)
        {
            if (potHasSeed)
            {
                currentCoinCount = 0; // set new current coin count for new round .
                coinProductionCoroutine = StartCoroutine(ProduceCoins());// start create coins.
            }
            yield return new WaitForSeconds(Random.Range(minDropInterval, maxDropInterval));// random time countdown to start drop coins.
            
        }
    }
    
    
    /// <summary>
    /// create dropping coins when coll this function
    /// </summary>
    /// <returns></returns>
    IEnumerator ProduceCoins()
    {
        while (true)
        {
            /*If the coins that have already been dropped have not reached the number required to be dropped per one time, they will continue to be dropped.*/
            if (currentCoinCount < CoinsPerTime)
            {
                int randomIndex = Random.Range(0, Prefab.Length);
                Instantiate(Prefab[randomIndex], transform.position, Quaternion.identity); // random create coin and exp.
                currentCoinCount++;
            }
            yield return new WaitForSeconds(coinProductionInterval);// cooldown time to create new coin.
            
        }
    } 

    

    private void OnDestroy()
    {
        GameManager.Instance.OnDayPass -= DayPass; // unsubscribe function.
        
        
        if (coinProductionCoroutine != null)
        {
            StopCoroutine(coinProductionCoroutine); // stop coroutine
        }
        if (RandomDropCoroutine != null)
        {
            StopCoroutine(RandomDropCoroutine);// stop coroutine
        }
    }
    
    /// <summary>
    /// Plant flower when player click plant button . it will load data in flower scriptable object that attach in each plant button 
    /// </summary>
    /// <param name="flower"></param>
    /// <param name="flowerItem"></param>
    public void PlantFlower(Flower flower,Item flowerItem)
    {
        int index = 0;
        flowerStore = flowerItem;
        flowerName = flower.flowername;
        _growstage = new int[flower.growstage.Length];
        flowerSprite = new Sprite[flower.sprites.Length];
        //collect sprites DATA
        foreach (var i in flower.sprites)
        {
            flowerSprite[index] = i;
            index++;
        }
        index = 0; // reset index for use collect grow stage DATA
        //collect grow stage dATA
        foreach (var i in flower.growstage)
        {
            _growstage[index] = i;
            index++;
        }
        _spriteRenderer.sprite = flowerSprite[0]; // plant seed flower sprite
        potHasSeed = true;
        timeChange = 0;
        _currentStage = 1;
        plantAge = 0;

    }
    /// <summary>
    ///  check flower stage every time if the day is passed, flower was watered and flower can grow. 
    ///  it will call UpdateSprite() for update flower to next grow stage.
    /// </summary>
    private void DayPass()
    {
        if (potHasSeed)
        {
            if (timeChange < _growstage.Length && watered)
            {
                plantAge++;
                UpdateSprite();
                //timeChange
                
            }
            else if(timeChange >= _growstage.Length)
            {
                maxFlowerGrow = true;
            }
            else if (timeChange >= _growstage.Length && watered) // if flower is can't grow anymore it will not call UpdateSprite().
            {
                plantAge++;
            }
            watered = false; //when day passed. reset water to false.
        }
        
    }
    /// <summary>
    /// check if flower age is enough for go to next stage
    /// </summary>
    private void UpdateSprite()
    {
        int index = 0;
        foreach (var i in _growstage)
        {
            if (plantAge == i) 
            {
                _spriteRenderer.sprite = flowerSprite[index+1];
                timeChange++;
                _currentStage ++;
                break;
            }
            index++;
            
        }
    }
    
    /// <summary>
    /// reset pot and sent flower to inventory
    /// </summary>
    public void Harvest()
    {
        _spriteRenderer.sprite = null;
        _growstage = null;
        _currentStage = 0;
        flowerName = null;
        plantAge = 0;
        potHasSeed = false;
        timeChange =0;
        watered = false;
        maxFlowerGrow = false;
        InventoryManager.Instance.Items.Add(flowerStore);
    }

    public void DeleteFlower()
    {
        _spriteRenderer.sprite = null;
        _growstage = null;
        _currentStage = 0;
        flowerName = null;
        plantAge = 0;
        potHasSeed = false;
        timeChange =0;
        watered = false;
        maxFlowerGrow = false;
    }
}
