using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class itemClick : MonoBehaviour
{
    public bool isStore;
    public int price;
    private int itemCount;
    public bool canSell;
    public Sprite flowerIcon;
    private Image sellBoxSprite;
    private TextMeshProUGUI textCoinSell;
    public Item flowerToSell;
    
    
    //when player click on the button check if it can sell or not
    public void OnClick()
    {
        
        if (isStore)
        {
            sellBoxSprite = GameObject.Find("SellBox_BackGround2").GetComponent<Image>();
            textCoinSell = GameObject.Find("TextCoinSell").GetComponent<TextMeshProUGUI>();
            //check if item can sell
            if (canSell)
            {
                Debug.Log("can sell");
                Debug.Log(flowerIcon);
                sellBoxSprite.sprite = flowerIcon;
                textCoinSell.text = price.ToString();
                InventoryManager.Instance.GetFlowerToSell(flowerToSell);
                
                //.Log( itemIndex);
            }
            else
            {
                //Debug.Log( itemIndex);
                Debug.Log("this item can't sell");
            }
        }
        else
        {
            Debug.Log("can't sell");
        }
    }

    private void Update()
    {
        
    }
}
