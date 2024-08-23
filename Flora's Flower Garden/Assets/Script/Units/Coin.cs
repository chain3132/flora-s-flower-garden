using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    private float destroyDeleyTime = 20f; // Countdown time before delete this game object.
    [SerializeField] private GameObject coinPopUp;
    
    private Coroutine destroyDeleyCoroutine;

    IEnumerator DestroyDeley()
    {
        yield return new WaitForSeconds(destroyDeleyTime);// Countdown continuously until the timer expires according to destroyDeleyTime.
        Destroy(gameObject); // delete this game object
    }

    private void Awake()
    {
       
    }

    void Start()
    {
        Rigidbody2D myRigidbody2D = GetComponent<Rigidbody2D>();
        
        float dropfoce = 2.1f; // force to push the coin out.
        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), 1.5f); // random direction everytime when the coin is out
        myRigidbody2D.AddForce(dropDirection * dropfoce,ForceMode2D.Impulse);// make the coin push out with random direction and force 
        
        destroyDeleyCoroutine = StartCoroutine(DestroyDeley()); //start Countdown for delete this game object
    }

    
    void OnMouseOver()
    {
        CoinSystem.Instance.GetCoin(3);//player gain x coin
        Destroy(gameObject);// destroy this game object
        AudioManager.Instance.PlaySFX(AudioManager.Instance.pickUpCoin);
        Instantiate(coinPopUp, transform.position, quaternion.identity);


    }

    private void OnDestroy()
    {
        if (destroyDeleyCoroutine != null)
        {
            StopCoroutine(destroyDeleyCoroutine);
        }
    }
}