using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinPopUp : MonoBehaviour
{
    private TextMeshPro coinText;
    private float disappearTime;
    private Color textColor;
    //private GameObject parantObject;

    private void Awake()
    {
        coinText = transform.GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        string text = "+" + "3 " + "coin";
        coinText.SetText(text);
        textColor = coinText.color;
        disappearTime = 0.5f;
    }

    private void Update()
    {
        float moveYspeed = 0.5f;
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime; // make coin text float up on the Y axis

        disappearTime -= Time.deltaTime;
        
        if (disappearTime <0 )
        {
            // text start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            coinText.color = textColor;
            if (textColor.a < 0 )
            {
                Destroy(gameObject);
                
            }
        }
    }
}
