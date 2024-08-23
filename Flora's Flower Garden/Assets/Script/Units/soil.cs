using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soil : MonoBehaviour
{
    public static soil Instance;
    [SerializeField] private Animator _animator; // tools animator
    [SerializeField] private movetools _movetools; // toolButton game object
    public bool isSoil ;
    public Texture2D cursorTexture; // Soil Texture
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    /// <summary>
    /// How to use : Attach this function to soil button
    /// when player click soil button it will change cursor to soil texture and close Tool UI
    /// </summary>
    public void OnClick()
    {
        _movetools.isRight = false;
        _animator.SetBool("goRight",false);
        _animator.SetBool("goleft",true); // close Tool UI 
        isSoil = true;
        Cursor.SetCursor(cursorTexture, Vector2.zero , CursorMode.Auto);
    }
}
