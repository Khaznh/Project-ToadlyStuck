using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnLevelManager : Singleton<SpawnLevelManager>
{
    private const int LEVEL_LENGTH = 19;

    [SerializeField] private List<GameObject> spawnedLevels = new();
    [SerializeField] private GameObject player;

    public GameObject currentLevelGameObject;
    public GameObject lastLevelGameObject;

    public int currentX = 0;
    private int currentLevel = 0;

    private void Start()
    {
        SpawnInit();
    }

    // do not start as level 3,8
    private void SpawnInit()
    {
        currentLevel = LevelChooseData.Instance.levelIndex;
        currentLevelGameObject = Instantiate(spawnedLevels[currentLevel - 1], new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(player, currentLevelGameObject.GetComponent<LVInfo>().playerSpawn.position, Quaternion.identity);
        currentX = 0;
    }    

    public void SpawnNextLevel()
    {
        currentLevel++;
        lastLevelGameObject = currentLevelGameObject;
        currentX = currentX + LEVEL_LENGTH;
        currentLevelGameObject = Instantiate(spawnedLevels[currentLevel - 1], new Vector3(currentX, 0, 0), Quaternion.identity);
        TelePlayer();
        MoveCameraManager.Instance.MoveCamera();
    }

    public void SpawnPreviousLevel()
    {
        currentLevel--;
        lastLevelGameObject = currentLevelGameObject;
        currentX = currentX - LEVEL_LENGTH;
        currentLevelGameObject = Instantiate(spawnedLevels[currentLevel - 1], new Vector3(currentX, 0, 0), Quaternion.identity);
        TelePlayer();
        MoveCameraManager.Instance.MoveCamera();
    }

    public void SetUp()
    {
        DestroyLastLevel();
    }

    private void TelePlayer()
    {
        PlayerController.Instance.TelePlayer(currentLevelGameObject.GetComponent<LVInfo>().playerSpawn.position);
    }

    private void DestroyLastLevel()
    {
        if (lastLevelGameObject == null)
        {
            return;
        }

        Destroy(lastLevelGameObject);
    }
}
