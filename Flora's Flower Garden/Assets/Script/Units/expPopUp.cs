using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class expPopUp : MonoBehaviour
{
    private TextMeshPro expText;
    private Color textColor;
    private float disappearTime;
    
    private void Awake()
    {
        expText = transform.GetComponent<TextMeshPro>();
    }
    
    private void Start()
    {
        string text = "+" + "2 " + "exp";
        expText.SetText(text);
        textColor = expText.color;
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
            expText.color = textColor;
            if (textColor.a < 0 )
            {
                Destroy(gameObject);
            }
        }
    }
}
