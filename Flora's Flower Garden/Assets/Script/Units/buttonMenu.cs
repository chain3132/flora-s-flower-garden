using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonMenu : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private GameObject animaButtonPrefab; // Prefab that has an animation to highlight the button menu.
    
    /*
     * Transform of the parent game object.
     * When the player points at this game object, which has this script attached, it will create an animation to highlight the button menu in the Transform. 
     */
    [SerializeField] private Transform mainMenu;
    private RectTransform _rectTransform;
    

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        Instantiate(animaButtonPrefab,_rectTransform.position,_rectTransform.rotation,mainMenu); //When Pointing, It will create an animation to highlight the button menu in the Transform. 
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hover);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimHandle.Instance.OnExitUI?.Invoke(); // When the player point Exit at this game object, It will call DestroyGameObject() in animbuttonMenu Script
    }
}
