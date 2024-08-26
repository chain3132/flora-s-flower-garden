 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 using System.Linq;

public class InventoryManager : MonoBehaviour , IDataPersistance
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public Transform ItemSellContent;
    public GameObject InventoryItem;   

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

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItemInGame(Item item)
    {
        Items.Remove(item);
    }

    public void ListItem()
    {
        //clean content in inventory before open;
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        List<Item> currentItems = new List<Item>();
        //add all item to content in inventory;
        foreach (var item in Items)
        {
            
            if (currentItems.Contains(item))// check if have same item
            {
                continue;
            }
            currentItems.Add(item);
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
            itemCount.text = Count(Items, item).ToString();
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            obj.GetComponent<itemClick>().isStore = false;
            

        }
    }
    public void ListItemStore()
    {
        //clean content in inventory before open;
        foreach (Transform item in ItemSellContent)
        {
            Destroy(item.gameObject);
        }
        
        List<Item> currentItems = new List<Item>();
        //add all item to content in inventory;
        foreach (var item in Items)
        {
            if (currentItems.Contains(item)) // check if have same item
            {
                continue;
            }
            currentItems.Add(item);
            
            GameObject obj = Instantiate(InventoryItem, ItemSellContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemCount.text = Count(Items, item).ToString();
            obj.GetComponent<itemClick>().isStore = true;
            obj.GetComponent<itemClick>().price = item.price;
            obj.GetComponent<itemClick>().canSell = item.canSell;
            obj.GetComponent<itemClick>().flowerIcon = item.icon;
            obj.GetComponent<itemClick>().flowerToSell = item;
            
        }
    }
    public int Count(List<Item> items, Item item) // Count the number of items available.
    {
        return items.Count(n => n == item);
    }
    public bool ContainsItem(string itemName)
    {
        foreach (var item in Items)
        {
            
            if (item.itemName == itemName)
            {
                //Item found in List!
                return true;
            }
        }
        //Item not found in List!
        return false;
    }

    private Item flowerToSell;
    public void GetFlowerToSell(Item flowerItem)
    {
        flowerToSell = flowerItem;
    }
    
    public void SellFlower()
    {
        if (Count(Items,flowerToSell) != 0)
        {
            RemoveItemInGame(flowerToSell);
            CoinSystem.Instance.GetCoin(flowerToSell.price);
            ListItemStore();
            GameObject.Find("SellBox_BackGround2").GetComponent<Image>().sprite = null;
            GameObject.Find("TextCoinSell").GetComponent<TextMeshProUGUI>().text = null;
        }
        else
        {
            
        }
         
    }

    public void LoadData(GameData data)
    {
        this.Items = data.Items;
    }

    public void SaveData(GameData data)
    {
        data.Items = this.Items;
    }
    
    
}
