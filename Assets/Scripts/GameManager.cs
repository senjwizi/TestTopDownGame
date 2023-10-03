using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public InventorySystem inventorySystem;
    private Data.GameData defaultGameData = new Data.GameData();

    private void Awake()
    {
        SaveSystem.Init();
        Load();
    }

    public void Save()
    {
        Data.GameData gameData = new Data.GameData();
        playerManager.GetData(ref gameData);
        enemyManager.GetData(ref gameData);
        inventorySystem.GetData(ref gameData);
        string jsonData = JsonUtility.ToJson(gameData);
        SaveSystem.Save(jsonData);
    }

    public void Load()
    {
        string loadedData = SaveSystem.Load();
        if (loadedData != null)
        {   
            Data.GameData loadedGameData = JsonUtility.FromJson<Data.GameData>(loadedData);
            InitGame(loadedGameData);
        }   
        else
        {
            InitGame(defaultGameData);
        }
    }

    private void InitGame(Data.GameData gameData)
    {
        playerManager.Init(gameData);
        enemyManager.Init(gameData);
        inventorySystem.Init(gameData);
    }

    void OnApplicationQuit()
    {
        Save();
    }
    
}
