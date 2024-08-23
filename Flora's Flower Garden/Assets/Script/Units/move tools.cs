using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movetools : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public bool isRight;

    public bool isleft;
    
    
    /// <summary>
    /// when player click at tool button it will call this function and play animation to slide UI tool block to Right
    /// when player click again it will slide UI tool block to left
    /// </summary>
    public void ButtonClick()
    {
        if (isRight == false)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
            anim.SetBool("goleft",false);
            anim.SetBool("goRight",true);
            isRight = true;
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
            anim.SetBool("goRight",false);
            anim.SetBool("goleft",true);
            isRight = false;
        }
        
    }
}
