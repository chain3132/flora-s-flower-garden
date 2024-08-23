using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class seedButton : MonoBehaviour
{
    [SerializeField] private Flower _flower;
    [SerializeField] private Item flowerStore;
    private GameObject seedPosition;
    private GameObject soilPosition;
    [SerializeField] private GameObject modelWindow;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private bool IsNotLock = false;
    [SerializeField] private Image _imageflower;
    [SerializeField] private GameObject LockImage;
    [SerializeField] private string flowerButtonName;

    private void Start()
    {
        GameManager.Instance.OnUnlockFlower += UnlockFlower; // subscribe function
        
        if (_imageflower != null)
        {
            _imageflower.GetComponent<Image>();
        }

        UnlockFlower(); // call for make sure to unlock flower that unlocked when enter game.
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUnlockFlower -= UnlockFlower;// unsubscribe function 
    }
    
    
    /// <summary>
    /// use this function for delete sprite that lock seed Image and set the flowers so they can plant.
    /// </summary>
    private void UnlockFlower()
    { 
        
        if (InventoryManager.Instance.ContainsItem(flowerButtonName))
        {
            IsNotLock = true;
            Destroy(LockImage);
            _imageflower.color =  Color.HSVToRGB(0,0,100);
        }
    }
    /// <summary>
    /// use this function by button UI for sent flower data to pot selected
    /// </summary>
    public void SentDataFlower()
    {
        
        seedPosition = FindChildWithTag(potSelected.Instance._selectedPot,"flowerPosition");
        soilPosition =  FindChildWithTag(potSelected.Instance._selectedPot,"soilPosition");
        if (!seedPosition.GetComponent<flowerPot>().potHasSeed && soilPosition.GetComponent<soilPots>().potHasSoil && IsNotLock) 
        {
            seedPosition.GetComponent<flowerPot>().PlantFlower(_flower,flowerStore);
            potSelected.Instance.seedUI.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.plantFlower);
        }
        else if (!IsNotLock)
        {
            modelWindow.SetActive(true);
            _textMeshProUGUI.text = "seed not unlock";
            potSelected.Instance.seedUI.SetActive(false);
        }
        else if (seedPosition.GetComponent<flowerPot>().potHasSeed)
        {
            modelWindow.SetActive(true);
            _textMeshProUGUI.text = "you're pot has already seed";
            potSelected.Instance.seedUI.SetActive(false);
        }
        else if(!soilPosition.GetComponent<soilPots>().potHasSoil)
        {
            modelWindow.SetActive(true);
            _textMeshProUGUI.text = "you're pot hasn't have soil";
            potSelected.Instance.seedUI.SetActive(false);
        }
        
        
    }
    
    /// <summary>
    /// use this function to find child game object in parent with tag
    /// and return child game object
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="childTag"></param>
    /// <returns></returns>
    GameObject FindChildWithTag(GameObject parent, string childTag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(childTag))
            {
                return child.gameObject;
            }
        }
        return null; 
    }

}
