using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    
    public GameObject Camera;//Main Camera
    public GameObject buttonBack;//ButtonGoToHome
    [SerializeField] private GameObject potUi;//IconpotUI
    [SerializeField] private GameObject seedUi;//seedUiPos
    [SerializeField] private GameObject toolUi;//toolsPanel
    [SerializeField] private GameObject flowerStoreButton;//flowerStoreButton
    [SerializeField] private GameObject furnitureStoreButton;//furnitureStoreButton
    [SerializeField] private GameObject flowerStorePanel;//flowerStore
    [SerializeField] private GameObject furnitureStorePanel;//funitureStore
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject buttonSideToLeft;//ButtonSideToLeft
    [SerializeField] private GameObject buttonSideToRight;//ButtonSideToRight
    public Vector3 rightSide = new Vector3(-40.9f,0,-10f);
    public Vector3 leftSide = new Vector3(-79.2f,0f,-10f);
    public Vector3 center = new Vector3(-60f,0f,-10f);
    public float speed = 1f; // pan camera speed
    private bool isCenter = true;

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    
    public void OnMouseDown()
    {
        if (!IsMouseOverUI())
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
            Camera.transform.position = new Vector3(-60,0,-10);
            buttonBack.SetActive(true);
            toolUi.SetActive(true);
            flowerStoreButton.SetActive(true);
            furnitureStoreButton.SetActive(true);
            buttonSideToRight.SetActive(true);
            buttonSideToLeft.SetActive(true);
        }
        
    }

    public void GoToHome()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        furnitureStorePanel.SetActive(false);
        flowerStorePanel.SetActive(false);
        Camera.transform.position = new Vector3(0,0,-10);
        potUi.SetActive(false);
        seedUi.SetActive(false);
        buttonBack.SetActive(false);
        toolUi.SetActive(false);
        buttonSideToRight.SetActive(false);
        buttonSideToLeft.SetActive(false);
    }
    
    IEnumerator PanCamera(Vector3 endPosition)
    {
        Vector3 startPosition = Camera.transform.position;
        
        float t = 0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * speed;
            Camera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
    }
    
    public void SideToLeft()
    {
        if (isCenter)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
            StartCoroutine(PanCamera(leftSide));
            buttonSideToLeft.SetActive(false);
            isCenter = false;
        }
        else if (!isCenter)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
            StartCoroutine(PanCamera(center));
            buttonSideToRight.SetActive(true);
            isCenter = true;
        }
    }
    public void SideToRight()
    {
        if (isCenter)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
            StartCoroutine(PanCamera(rightSide));
            buttonSideToRight.SetActive(false);
            isCenter = false;
        }
        else if (!isCenter)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
            StartCoroutine(PanCamera(center));
            buttonSideToLeft.SetActive(true);
            isCenter = true;
        }
    }

    
    public void GoToFlowerStore()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        buttonBack.SetActive(true);
        furnitureStorePanel.SetActive(false);
        flowerStorePanel.SetActive(true);
        toolUi.SetActive(false);
        buttonSideToRight.SetActive(false);
        buttonSideToLeft.SetActive(false);
        Camera.transform.position = new Vector3(-33.5f, -22.72f, -10f);
    }
    public void GoToFunitureStore()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        buttonBack.SetActive(true);
        flowerStorePanel.SetActive(false);
        furnitureStorePanel.SetActive(true);
        toolUi.SetActive(false);
        buttonSideToRight.SetActive(false);
        buttonSideToLeft.SetActive(false);
        Camera.transform.position = new Vector3(5.1f, -22.72f, -10f);
    }
    
    
}
