using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandle : MonoBehaviour
{
    public static AnimHandle Instance; // set instance

    public Action OnExitUI;

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
}
