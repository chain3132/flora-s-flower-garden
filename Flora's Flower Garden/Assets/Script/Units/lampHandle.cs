using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class lampHandle : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        if (!IsMouseOverUI()) // If the mouse is not over a UI element, the TriggerNextDay method of the GameManager instance is called,
        {
            //GameManager.Instance.TriggerNextDay();
        }
        
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
