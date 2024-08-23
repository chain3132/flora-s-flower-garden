using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class CoinSystem : MonoBehaviour, IDataPersistance
{
    
    public static CoinSystem Instance;
    [ReadOnly]public int currentCoin;
    [SerializeField] private TextMeshProUGUI coinText;
    
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
        coinText.text = currentCoin.ToString();
    }

    public void GetCoin(int coin)
    {
        currentCoin += coin;
        coinText.text = currentCoin.ToString();
    }
    public void LoadData(GameData data)
    {
        currentCoin = data.currentCoin;
    }

    public void SaveData(GameData data)
    {
        data.currentCoin = currentCoin;
    }
}
