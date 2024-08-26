using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeSetting : MonoBehaviour,IDataPersistance
{
    public static volumeSetting Instance;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private float sfxVolume;
    private float musicVolume;

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
        SetMusicVolume();
        SetSfxVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        _mixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        _mixer.SetFloat("sfx", Mathf.Log10(volume)*20);
    }

    public void LoadVolume(float musicvolume,float sfxvolume)
    {
        musicSlider.value = musicvolume;
        sfxSlider.value = sfxvolume;
        SetMusicVolume();
        SetSfxVolume();
    }

    public void LoadData(GameData data)
    {
        sfxVolume = data._sfxVolume;
        musicVolume = data._musicVolume;
        LoadVolume(musicVolume, sfxVolume);
    }

    public void SaveData(GameData data)
    {
        data._sfxVolume = musicSlider.value;
        data._musicVolume = sfxSlider.value;
    }
}
