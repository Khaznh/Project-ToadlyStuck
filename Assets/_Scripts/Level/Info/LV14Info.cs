using UnityEngine;

public class LV14Info : LVInfo
{
    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 14);
    }

    public void OnPlayerNextLevel(Collider2D collision, Activer activer)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        LevelUnlockManager.Instance.UnlockNextLevel(15);
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
