using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class animbuttonMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;// animation highlight button
    
    void Start()
    {
        animator.SetBool("isPlayed",true); // play animation highlight button when player pointing it.
        
        /* **Be careful** The animation must play first before subscribe function. Otherwise, there is a chance it will be deleted prematurely. */
        AnimHandle.Instance.OnExitUI += DestroyGameObject;  // subscribe function
    }
    /// <summary>
    /// delete this object when this func is call.
    /// </summary>
    void DestroyGameObject(){Destroy(gameObject);}

    private void OnDestroy()
    {
        AnimHandle.Instance.OnExitUI -= DestroyGameObject; // unsubscribe function
    }
}
