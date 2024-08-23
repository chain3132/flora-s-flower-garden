using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPotSelected : MonoBehaviour
{
    [SerializeField] private Animator animator;// animation highlight PotSelected
    
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isPlaying",true);
        potSelected.Instance.OnExitPot += DestroyGameObject;
    }
    
    /// <summary>
    /// delete this object when this func is call.
    /// </summary>
    void DestroyGameObject(){Destroy(gameObject);}

    private void OnDestroy()
    {
        potSelected.Instance.OnExitPot -= DestroyGameObject;
    }
}
