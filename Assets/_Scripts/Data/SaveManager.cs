using System;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string savePath;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject); 
        savePath = Application.persistentDataPath + "/gamedata.save";
    }

    public GameData LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string data = File.ReadAllText(savePath);

                return JsonUtility.FromJson<GameData>(data);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                return new GameData();
            }
        }
        else
        {
            return new GameData();
        }
    }

    public void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);

        File.WriteAllText(savePath, json);
        Debug.Log("Saved data in: " + savePath);
    }
}
