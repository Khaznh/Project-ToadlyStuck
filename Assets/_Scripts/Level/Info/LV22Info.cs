using System;
using UnityEngine;

public class LV22Info : LVInfo
{
    [SerializeField] private SwipeManager swipeManager;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 22);
        swipeManager.OnSwipeLeft += HandleSwipeLeft;
    }

    private void HandleSwipeLeft()
    {
        LevelUnlockManager.Instance.UnlockNextLevel(23);
        SpawnLevelManager.Instance.SpawnNextLevel();
    }

    public void OnPlayerSpike(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        PlayerController.Instance.playerDeath.Die();
        PlayerController.Instance.transform.position = playerSpawn.position;
    }
}
