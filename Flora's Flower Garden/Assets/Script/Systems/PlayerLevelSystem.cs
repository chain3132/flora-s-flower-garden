using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevelSystem : MonoBehaviour,IDataPersistance
{
    public static PlayerLevelSystem Instance;
    
    public int playerLevel = 1;
    private float currentXP = 0f;
    public int[] xpThresholds;
    [SerializeField] private Image Expbar ; // ExpBar Image
    public delegate void OnLevelUp(int newLevel);
    public event OnLevelUp onLevelUp;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject LevelPOPUp;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        // Initialize XP thresholds
        xpThresholds = new int[] { 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500, 5500 };
        levelText.GetComponent<TextMeshProUGUI>();
        
    }

    private void Start()
    {
        levelText.text = playerLevel.ToString();
        Expbar.fillAmount = currentXP / xpThresholds[playerLevel - 1];
    }

    

    public void AddXP(int amount)
    {
        currentXP += amount;
        Expbar.fillAmount = currentXP / xpThresholds[playerLevel - 1];
        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        if (playerLevel < xpThresholds.Length && currentXP >= xpThresholds[playerLevel - 1])
        {
            playerLevel++;
            currentXP = 0;
            Expbar.fillAmount = 0;  
            levelText.text = playerLevel.ToString();
            LevelPOPUp.SetActive(true);
            AudioManager.Instance.PlaySFXLevel(AudioManager.Instance.levelUp);
            
            
            if (onLevelUp != null)
            {
                onLevelUp(playerLevel);
            }
            // Handle level-up rewards here
            
        }
    }

    

    public void LoadData(GameData data)
    {
        this.playerLevel = data.playerLevel;
        this.currentXP = data.currentExp;
    }

    public void SaveData( GameData data)
    {
        data.playerLevel = this.playerLevel;
        data.currentExp = this.currentXP;
    }
}
