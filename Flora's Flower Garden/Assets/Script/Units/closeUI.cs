using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeUI : MonoBehaviour
{
    [SerializeField] private GameObject UI; // UI that you want to close 
    [SerializeField] private GameObject UiToShow;// UI that you want to show 
    [SerializeField] private GameObject inventory;
    

    public void OnClick()
    {
        UI.SetActive(false); // close UI when player click game object 
        AudioManager.Instance.PlaySFX(AudioManager.Instance.closeUi);
    }

    public void showUi()
    {
        UiToShow.SetActive(true); // show UI when player click game object
    }

    public void ShowInventory()
    {
        inventory.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.openInventory);
    }
}
