using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<int> unlockedLevel = new();

    public void UnlockedLevel(int levelIndex)
    {
        if (IsUnlockedLevel(levelIndex))
        {
            return;
        }

        unlockedLevel.Add(levelIndex);
    }

    public bool IsUnlockedLevel(int levelIndex)
    {
        return unlockedLevel.Contains(levelIndex);
    }

    public List<int> GetAllUnlockedLevel()
    {
        return unlockedLevel;
    }

    public GameData()
    {
        unlockedLevel.Add(1);
    }
}
