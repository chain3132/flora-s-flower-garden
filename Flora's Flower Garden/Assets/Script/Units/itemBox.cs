using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class itemBox : MonoBehaviour
{
    
    [SerializeField] private Item _item;
    [SerializeField] private GameObject modelWindow;
    [SerializeField] private Image modelImageWindow;
    
    public void SentItem()
    {
        BuySystem.Instance.currentItemSelected = _item;
        modelWindow.SetActive(true);
        modelImageWindow.sprite = _item.icon;

    }
}
