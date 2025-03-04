using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;

    public static Action OnHourChanged;
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 0.5f;

    private float timer;
    // Start is called before the first frame update
   
    

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            
            if (Minute >=60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
                
            }
            
            timer = minuteToRealTime;
        }
    }

    
}
