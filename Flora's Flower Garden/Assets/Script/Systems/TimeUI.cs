using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private const float REAL_SECONDS_PER_INGAME_DAY = 60f;
    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private TextMeshProUGUI timeText;
    private float day =0.33f;

    [SerializeField] private Animator _light2D;
    private void Awake()
    {
        _light2D = GetComponent<Animator>();
        clockHourHandTransform = transform.Find("clockHand");
        clockMinuteHandTransform = transform.Find("minuteHand");
        timeText = transform.Find("timeText").GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        if (_light2D == null)
        {
            _light2D = GameObject.Find("Light 2D").GetComponent<Animator>();
        }
    }

    private void Update()
    {
        
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;

        float rotationDegreesPerDay = 360f ;
        float hourHandRotation = dayNormalized * rotationDegreesPerDay  ;
        clockHourHandTransform.eulerAngles = new Vector3(0, 0, -hourHandRotation*1.98f );
        
        
        float hoursPerDay = 24f;
        clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay);
        
        string hoursString = Mathf.Floor(dayNormalized * 24f).ToString("00");
        
        
        float minutesPerHour = 60f;
        string minuteString = Mathf.Floor((dayNormalized * hoursPerDay %1f) * minutesPerHour).ToString("00");
        
        //change day when time is 23.59
        string resetDay = hoursString + ":" + minuteString;
        if (resetDay == "23:59")
        {
            GameManager.Instance.TriggerNextDay();
            _light2D.SetBool("isNight",true);
        }
        else if (resetDay == "07:00")
        {
            _light2D.SetBool("isNight",false);
        }

        timeText.text = hoursString + ":" + minuteString;
    }
}
