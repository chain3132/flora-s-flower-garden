using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCan : MonoBehaviour // this script should attach at watering can prefab
{
    [SerializeField] private Animator _animator; // watering animator
    void OnEnable()
    {
        GameManager.Instance.WateringFlower += WateringAnimation; // subscribe function
    }

    private void OnDestroy()
    {
        GameManager.Instance.WateringFlower -= WateringAnimation; // unsubscribe function
    }
    
    void WateringAnimation()
    {
        _animator.SetBool("watering",true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.watering);
        StartCoroutine(DeletePrefabDelayed(1.4f));

    }
    //countdown time delete this object
    private IEnumerator DeletePrefabDelayed(float timedalay)
    {
        yield return new WaitForSeconds(timedalay);
        _animator.SetBool("watering",false);
        Destroy(gameObject);
    }
    
}
