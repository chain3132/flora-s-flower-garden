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
    [SerializeField] private Image _imageflower;
    

    private void Start()
    {
        
        if (_imageflower != null)
        {
            _imageflower.GetComponent<Image>();
        }
       
    }

    
    
    
    /// <summary>
    /// use this function by button UI for sent flower data to pot selected
    /// </summary>
    public void SentDataFlower()
    {
        seedPosition = FindChildWithTag(potSelected.Instance._selectedPot,"flowerPosition");
        soilPosition =  FindChildWithTag(potSelected.Instance._selectedPot,"soilPosition");
        if (!seedPosition.GetComponent<flowerPot>().potHasSeed && soilPosition.GetComponent<soilPots>().potHasSoil ) 
        {
            seedPosition.GetComponent<flowerPot>().PlantFlower(_flower,flowerStore);
            potSelected.Instance.seedUI.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.plantFlower);
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
