using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class BuySystem : MonoBehaviour
{
    public static BuySystem Instance;
    public Item currentItemSelected = null;
    [SerializeField] private GameObject modelBuyWindow;
    [SerializeField] private GameObject modelWindow;
    [SerializeField] private TextMeshProUGUI modelWindowtext;

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

    public void BuyItem()
    {
        if (CoinSystem.Instance.currentCoin >= currentItemSelected.price)
        {
            modelBuyWindow.SetActive(false);
            CoinSystem.Instance.ReduceCoins(currentItemSelected.price);
            InventoryManager.Instance.Add(currentItemSelected);
        }
        else
        {
            modelBuyWindow.SetActive(false);
            modelWindow.SetActive(true);
            modelWindowtext.text = "coin is not enough";
        }
    }
}
