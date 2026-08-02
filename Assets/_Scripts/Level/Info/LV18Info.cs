using UnityEngine;

public class LV18Info : LVInfo
{
    public void OnEnable()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", 18);
    }

    public void OnDisable()
    {
        LevelUnlockManager.Instance.UnlockNextLevel(19);
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
