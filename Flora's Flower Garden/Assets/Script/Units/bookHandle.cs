using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bookHandle : MonoBehaviour
{
    [SerializeField] private GameObject bookUI;
    
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(); // check is player pointing over Ui
    }
    private void OnMouseDown()
    {
        if (!IsMouseOverUI()) // 
        {
            bookUI.SetActive(true);
        }
    }

    

    
}
