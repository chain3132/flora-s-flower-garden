
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    /* ***warning***
     --------------
     don't destroy on load () can not use in this script and all script that this script attached
     --------------
     */
    public static GameManager Instance;
    
    public int dayInGame = 1;
    public event Action OnDayPass;
    public event Action WateringFlower;
    public event Action OnUnlockFlower;
    
    [SerializeField] private GameObject prefab;
    [SerializeField] private Item[] _item;
    
    

    [Header("FlowerTree")] 
    [SerializeField] private Image[] lockUi;
    [SerializeField] private Image[] flowerUi;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        PlayerLevelSystem.Instance.onLevelUp += OnPlayerLevelUp;
    }

    void OnPlayerLevelUp(int newLevel)
    {
        
        if (newLevel == 2)
        {
            InventoryManager.Instance.Add(_item[0]); // add Daisy seed
        }
        else if (newLevel == 3)
        {
            InventoryManager.Instance.Add(_item[1]); // add Rose seed
        }
        else if (newLevel == 4)
        {
            InventoryManager.Instance.Add(_item[2]); // add Tulip seed
        }
        else if (newLevel == 5)
        {
            InventoryManager.Instance.Add(_item[3]);// add violet seed
        }
        else if (newLevel == 6)
        {
            InventoryManager.Instance.Add(_item[4]); // add Carnation seed
        }
        OnUnlockFlower?.Invoke();
        UnlockFlowerUI(newLevel);
        // -- TODO --
        // - make level up pop up when player level up and show if there' re something unlock
    }
    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        PlayerLevelSystem.Instance.onLevelUp -= OnPlayerLevelUp;
    }
    
    public void TriggerNextDay()
    {
        OnDayPass?.Invoke();
    }

    public void Watering()
    {
        Instantiate(prefab, potSelected.Instance._selectedPot.transform.position + Vector3.up * 1.5f +Vector3.right *0.5f ,potSelected.Instance._selectedPot.transform.rotation);
        WateringFlower?.Invoke();
    }

    void UnlockFlowerUI(int playerLevel)
    {
        Destroy(lockUi[playerLevel - 2]);
        flowerUi[playerLevel - 2].color = Color.HSVToRGB(0,0,100);
    }

    
}
