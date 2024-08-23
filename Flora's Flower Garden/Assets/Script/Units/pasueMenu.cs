using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pasueMenu : MonoBehaviour
{
    private bool isPause;
    [SerializeField] private GameObject OptionPanel; //  Option Panel UI (always set active close).
    
    
    void Update()
    {
        // if player press Esc if not pause it will call PauseGame if it pause it will continue game.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        OptionPanel.SetActive(true); // show Option Panel UI.
        Time.timeScale = 0f; // stop anything in game .
        AudioManager.Instance.BackgroundSource.Pause();
        isPause = true;
    }
    
    public void ResumeGame()
    {
        OptionPanel.SetActive(false); // close Option Panel UI.
        Time.timeScale = 1f;// continue game.
        isPause = false;
        AudioManager.Instance.BackgroundSource.UnPause();
    }

    public void GoToMainMenu()
    {
        isPause = false;
        Time.timeScale = 1f;// continue game.
        DataPersistanceManager.instance.SaveGame(); // call save function for save all game data when exit to main menu.
        SceneManager.LoadSceneAsync(0); // Load scene 0 (MainMenu).
        AudioManager.Instance.BackgroundSource.clip = AudioManager.Instance.mainMenu;
        AudioManager.Instance.BackgroundSource.Play();
    }

    public void _SaveGame()
    {
        // call save function for save all game data when player click save button UI.
        DataPersistanceManager.instance.SaveGame();
    }
}
