using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockManager : Singleton<LevelUnlockManager>
{
    private GameData currentGameData;

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        currentGameData = SaveManager.Instance.LoadGame();
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        return currentGameData.IsUnlockedLevel(levelIndex);
    }

    public void UnlockNextLevel(int levelIndex)
    {
        currentGameData.UnlockedLevel(levelIndex);
        SaveManager.Instance.SaveGame(currentGameData);
    }

    public List<int> GetUnlockLevel()
    {
        return currentGameData.GetAllUnlockedLevel();
    }
}
