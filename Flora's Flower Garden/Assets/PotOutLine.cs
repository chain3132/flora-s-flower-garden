using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOutLine : MonoBehaviour
{
    [SerializeField] private Transform potTransform;
    [SerializeField] private GameObject animaButtonPrefab; // Prefab that has an animation to highlight the potSelected.
    private void OnMouseEnter()
    {
        Instantiate(animaButtonPrefab, transform.position + Vector3.up *0.5f, Quaternion.identity, potTransform);
    }

    private void OnMouseExit()
    {
        potSelected.Instance.OnExitPot?.Invoke();
    }
}
