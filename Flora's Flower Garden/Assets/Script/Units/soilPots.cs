using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class soilPots : MonoBehaviour,IDataPersistance
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField]private Sprite _sprite; // soil sprite
    public bool potHasSoil;
    [Header("GenerateId")]
    [SerializeField] private string id;
    
    
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    public void LoadData(GameData data)
    {
        foreach (var SoilInPot in data.soilInPot)  // load each data to find data that has the same id
        {
            if (SoilInPot.id == id) // check is save soil id = soil id 
            {
                potHasSoil = SoilInPot.potHasSoil; // save bool if save soil has soil or not
            }
        }
        
        if (potHasSoil)
        {
            _spriteRenderer.sprite = _sprite; // set soil position sprite to soil sprite
        }
    }

    public void SaveData( GameData data)
    {
        if (data.soilInPot == null)
        {
            data.soilInPot = new List<SoilInPot>();
        }

        SoilInPot existingSoilInPot = data.soilInPot.Find(pot => pot.id == id); // find old save data and return data
        if (existingSoilInPot != null) // have old data
        {
            //Overwrite data
            existingSoilInPot.id = this.id;
            existingSoilInPot.potHasSoil = this.potHasSoil;
        }
        else// have no old data
        {
            //set the current data
            SoilInPot soilInPot  = new SoilInPot();
            soilInPot.id = this.id;
            soilInPot.potHasSoil = this.potHasSoil;
            // save new data
            data.soilInPot.Add(soilInPot);
        }
        
    }
/// <summary>
/// when this function is using it will set soil position sprite to soil sprite
/// </summary>
    public void soil()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.soil);
        potHasSoil = true;
        _spriteRenderer.sprite = _sprite;
    }
}
