using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class watering : MonoBehaviour
{
    public static watering Instance;
    
    [SerializeField] private Animator _animator;// tool animator
    [SerializeField] private movetools _movetools; // toolbutton
    public bool isWatering ;
    public Texture2D cursorTexture; // watering can texture
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
/// <summary>
/// How to use : Attach this function to watering button
/// when player click watering button it will change cursor to watering can texture and close Tool UI
/// </summary>
    public void OnClick()
    {
        _movetools.isRight = false;
        _animator.SetBool("goRight",false);
        _animator.SetBool("goleft",true);
        isWatering = true;
        Cursor.SetCursor(cursorTexture, Vector2.zero , CursorMode.Auto);
    }
}
