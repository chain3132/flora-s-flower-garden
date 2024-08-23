using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button continueGameButton;
    [SerializeField] private TextMeshProUGUI _textMeshPro;// continue text

   

    private void Start()
    {
        if (!DataPersistanceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            Color color = _textMeshPro.color;
            color.a = 0.326f;
            _textMeshPro.color = color;
        }
    }
    
    public void StartGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        AudioManager.Instance.ChangeSong(Random.Range(0, AudioManager.Instance.BackgroundSong.Length));
        DataPersistanceManager.instance.NewGame();
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(1); // load homepage scene
    } 

    public void ContinueGame()
    {
        AudioManager.Instance.ChangeSong(Random.Range(0, AudioManager.Instance.BackgroundSong.Length));
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        SceneManager.LoadSceneAsync(1);// load homepage scene
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    
    
}
