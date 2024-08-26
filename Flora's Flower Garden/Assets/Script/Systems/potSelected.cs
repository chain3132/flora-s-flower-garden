using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class potSelected : MonoBehaviour
{
    public static potSelected Instance;
    public Action OnExitPot;
    
    [SerializeField]private GameObject UiPot;
    [SerializeField]public GameObject seedUI;
    [SerializeField] private GameObject InfoPot;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject modelWindow;
    [SerializeField] private GameObject footherModelWindow;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    
    public GameObject _selectedPot;
    private GameObject seedPosition;
    public GameObject soilPosition;
    private soilPots soilPots;
    
    
    [SerializeField]private GameObject flowerImage;
    [SerializeField]private GameObject flowerName;
    [SerializeField]private GameObject growth;
    [SerializeField]private GameObject water;
    [SerializeField]private GameObject fertilizer;
    [SerializeField] private GameObject age;
    [SerializeField]private Button harvestButton;

    
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        // growth.GetComponent<TextMeshPro>();
        // water.GetComponent<TextMeshPro>();
        // fertilizer.GetComponent<TextMeshPro>();
       
    }
    

    private void Update()
    {
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition = new Vector2(mousePosition3D.x, mousePosition3D.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition,Vector2.zero);
        
        if (EventSystem.current.IsPointerOverGameObject()) {
            return; 
        }
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("pots"))
            {
                _selectedPot = hit.collider.gameObject;
                soilPosition =  FindChildWithTag(_selectedPot,"soilPosition");
                seedPosition = FindChildWithTag(_selectedPot,"flowerPosition");
                if (Input.GetKey(KeyCode.E) && !watering.Instance.isWatering)
                {
                    seedPosition = FindChildWithTag(_selectedPot,"flowerPosition");
                    UiPot.SetActive(true); // showPotUi 
                    seedUI.SetActive(false); //close seed ui
                    //set Ui position on top pot position
                    Vector3 uiPotPos = Camera.main.WorldToScreenPoint(_selectedPot.transform.position + Vector3.up * 1f);
                    UiPot.transform.position = uiPotPos;
                }
                else if (watering.Instance.isWatering && Input.GetMouseButtonDown(0) && soilPosition.GetComponent<soilPots>().potHasSoil )
                {
                    seedPosition.GetComponent<flowerPot>().watered = true;
                    GameManager.Instance.Watering();
                }
                else if (soil.Instance.isSoil && Input.GetMouseButtonDown(0) && !soilPosition.GetComponent<soilPots>().potHasSoil)
                {
                    soilPosition.GetComponent<soilPots>().soil();
                }
                
            }
            
        }

        if (Input.GetMouseButtonDown(1) && watering.Instance.isWatering || Input.GetMouseButtonDown(1) && soil.Instance.isSoil)
        {
            watering.Instance.isWatering = false;
            soil.Instance.isSoil = false;
            Cursor.SetCursor(default,Vector2.zero,CursorMode.Auto);
        }
    }

    
    public void ActivePotUI()
    {
        //close PotUi
        UiPot.SetActive(false);
        seedUI.SetActive(true);// showSeedUi 
        //set Ui position on top pot position
        Vector3 seedUiPosition = Camera.main.WorldToScreenPoint(_selectedPot.transform.position + Vector3.up * 2f);
        seedUI.transform.position = seedUiPosition;

    }

    public void ActiveInfoUI()
    {
        UiPot.SetActive(false);
        InfoPot.SetActive(true); // showInfoUi
        backButton.SetActive(false);
        flowerImage.GetComponent<Image>().sprite = seedPosition.GetComponent<flowerPot>()._spriteRenderer.sprite;
        flowerName.GetComponent<TextMeshProUGUI>().text = seedPosition.GetComponent<flowerPot>().flowerName ;
        age.GetComponent<TextMeshProUGUI>().text = seedPosition.GetComponent<flowerPot>().plantAge.ToString();
        if (seedPosition.GetComponent<flowerPot>().watered)
        {
            water.GetComponent<TextMeshProUGUI>().text = "already watered";
        }
        else
        {
            water.GetComponent<TextMeshProUGUI>().text = "haven't been watered yet";
        }
        growth.GetComponent<TextMeshProUGUI>().text = seedPosition.GetComponent<flowerPot>()._currentStage.ToString();
        harvestButton.interactable = false;
        //check if it's can harvest
        if (seedPosition.GetComponent<flowerPot>().maxFlowerGrow)
        {
            harvestButton.interactable = true;
        }

    }

    
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

    public void Harvesting()
    {
        seedPosition.GetComponent<flowerPot>().Harvest();
    }

    public void DeleteFlower()  
    {
        if (seedPosition.GetComponent<flowerPot>().potHasSeed)
        {
            UiPot.SetActive(false);
            modelWindow.SetActive(true);
            footherModelWindow.SetActive(true);
            _textMeshProUGUI.text = "Are you sure to delete this flower ?";
        }
        else
        {
            UiPot.SetActive(false);
            modelWindow.SetActive(true);
            _textMeshProUGUI.text = "you're pot has no seed";
        }
        
        
    }

    public void ConfirmDelete()
    {
        modelWindow.SetActive(false);
        footherModelWindow.SetActive(false);
        seedPosition.GetComponent<flowerPot>().DeleteFlower();
    }

    public void NotConfirmDelete()
    {
        modelWindow.SetActive(false);
        footherModelWindow.SetActive(false);
    }
}
