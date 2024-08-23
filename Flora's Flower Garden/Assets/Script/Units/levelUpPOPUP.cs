using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class levelUpPOPUP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void OnEnable()
    {
        
        _textMeshProUGUI.text = PlayerLevelSystem.Instance.playerLevel.ToString();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            
            gameObject.SetActive(false);
        }
    }
}
