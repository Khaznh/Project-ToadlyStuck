using UnityEngine;

public class LV13Info : LVInfo
{
    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 13);
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

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        LevelUnlockManager.Instance.UnlockNextLevel(14);
        SpawnLevelManager.Instance.SpawnNextLevel();
    }
}
