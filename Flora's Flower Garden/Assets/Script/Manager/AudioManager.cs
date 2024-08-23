using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Source")] 
    public AudioSource BackgroundSource;
    public AudioSource SFXSource;
    public AudioSource LevelSource;
    [Header("Audio Clip")]
    public AudioClip openInventory;
    public AudioClip[] BackgroundSong;
    public AudioClip hover;
    public AudioClip click;
    public AudioClip closeUi;
    public AudioClip mainMenu;
    public AudioClip watering;
    public AudioClip soil;
    public AudioClip plantFlower;
    public AudioClip pickUpCoin;
    public AudioClip pickUpExp;
    public AudioClip levelUp;

    private float _trackTimer;
    
        

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        BackgroundSource.clip = mainMenu;
        BackgroundSource.Play();
    }

    private void Update()
    {
        if (BackgroundSource.isPlaying)
        {
            _trackTimer += 1 * Time.deltaTime;
        }

        if (!BackgroundSource.isPlaying || _trackTimer >= BackgroundSource.clip.length)
        {
            ChangeSong(Random.Range(0,BackgroundSong.Length));
        }
    }

    public void ChangeSong(int songPicked)
    {
        _trackTimer = 0;
        BackgroundSource.clip = BackgroundSong[songPicked];
        BackgroundSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }
    
    public void PlaySFXLevel(AudioClip clip)
    {
        LevelSource.clip = clip;
        LevelSource.Play();
    }
    
    
        
}
