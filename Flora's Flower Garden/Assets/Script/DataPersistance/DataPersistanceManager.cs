using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string FileName;
    private GameData _gameData;
    
    public List<IDataPersistance> DataPersistancesObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if (instance!= null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, FileName);
        
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        this.DataPersistancesObjects = FindAllDataPersistanceObjects();
        LoadGame();
        
    }
    

    public void NewGame()
    {
        this._gameData = new GameData();
    }
    public void LoadGame()
    {
        this._gameData = _dataHandler.Load();
        if (this._gameData == null)
        {
            Debug.LogWarning("No data was found.A new game need to be  started before data can be loaded");
            return;
        }
        
        foreach (IDataPersistance dataPersistanceObj in DataPersistancesObjects)
        {
            dataPersistanceObj.LoadData(_gameData);
        }
        
        
    }
    public void SaveGame()
    {
        if (this._gameData == null)
        {
            Debug.LogWarning("No Data was found");
            return;
        }
        
        //
        foreach (IDataPersistance dataPersistanceObj in DataPersistancesObjects)
        {
            dataPersistanceObj.SaveData( _gameData);
        }
        
        _dataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    
    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> DataPersistancesObjects =
            FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistance>();
        return new List<IDataPersistance>(DataPersistancesObjects);
    }

    public bool HasGameData()
    {
        return _gameData != null;
    }
}
